using Shuttle.Abacus.Events.Matrix.v1;
using Shuttle.Contract;

namespace Shuttle.Abacus;

public class Matrix
{
    private readonly List<Constraint> _constraints = [];
    private readonly List<Element> _elements = [];

    public Guid Id { get; set; }

    public string DataTypeName { get; private set; } = string.Empty;

    public string Name { get; private set; } = string.Empty;

    public IEnumerable<Element> Elements => _elements.AsReadOnly();
    public IEnumerable<Constraint> Constraints => _constraints.AsReadOnly();

    public Guid RowArgumentId { get; private set; }
    public Guid? ColumnArgumentId { get; private set; }
    public bool Removed { get; private set; }

    public Registered Register(string name, Guid rowArgumentId, Guid? columnArgumentId, string dataTypeName)
    {
        Guard.AgainstEmpty(name);

        return On(new Registered
        {
            Name = name,
            RowArgumentId = rowArgumentId,
            ColumnArgumentId = columnArgumentId,
            DataTypeName = dataTypeName
        });
    }

    private Registered On(Registered registered)
    {
        Guard.AgainstNull(registered);

        Name = registered.Name;
        RowArgumentId = registered.RowArgumentId;
        ColumnArgumentId = registered.ColumnArgumentId;
        DataTypeName = registered.DataTypeName;

        _constraints.Clear();
        _elements.Clear();

        return registered;
    }

    public static string Key(string name)
    {
        return $"[matrix]:name={name}";
    }

    public ElementRegistered RegisterElement(Guid id, int row, int column, string value)
    {
        return On(new ElementRegistered
        {
            Id = id,
            Row = row,
            Column = column,
            Value = value
        });
    }

    private ElementRegistered On(ElementRegistered elementRegistered)
    {
        Guard.AgainstNull(elementRegistered);

        _elements.RemoveAll(item => item.Row == elementRegistered.Row && item.Column == elementRegistered.Column);
        _elements.Add(new(elementRegistered.Id, elementRegistered.Row, elementRegistered.Column, elementRegistered.Value));

        return elementRegistered;
    }

    public bool IsNamed(string name)
    {
        return Name.Equals(name, StringComparison.InvariantCultureIgnoreCase);
    }

    public ConstraintRegistered RegisterConstraint(Guid id, string axis, int index, string comparison, string value)
    {
        Guard.AgainstEmpty(axis);
        Guard.AgainstEmpty(comparison);

        return On(new ConstraintRegistered
        {
            Id = id,
            Axis = axis,
            Index = index,
            Comparison = comparison,
            Value = value
        });
    }

    private ConstraintRegistered On(ConstraintRegistered constraintRegistered)
    {
        Guard.AgainstNull(constraintRegistered);

        _constraints.RemoveAll(item =>
            item.Axis.Equals(constraintRegistered.Axis, StringComparison.InvariantCultureIgnoreCase) &&
            item.Index == constraintRegistered.Index);

        _constraints.Add(new(constraintRegistered.Id, constraintRegistered.Axis, constraintRegistered.Index,
            constraintRegistered.Comparison, constraintRegistered.Value));

        return constraintRegistered;
    }

    public string GetValue(IValueComparer valueComparer, ExecutionContext executionContext, Argument rowArgument, Argument? columnArgument)
    {
        Guard.AgainstNull(valueComparer);
        Guard.AgainstNull(executionContext);
        Guard.AgainstNull(rowArgument);

        if (ColumnArgumentId.HasValue)
        {
            Guard.AgainstNull(columnArgument);
        }

        var row = FindConstraint("Row", valueComparer, rowArgument.DataType, executionContext.GetArgumentValue(RowArgumentId));
        var column = ColumnArgumentId.HasValue
            ? FindConstraint("Column", valueComparer, columnArgument!.DataType, executionContext.GetArgumentValue(ColumnArgumentId.Value))
            : 1;

        var element = _elements.FirstOrDefault(item => item.Row == row && item.Column == column);

        if (element == null)
        {
            throw new InvalidOperationException(
                $"Could not an element for matrix '{Name}' at intersection of row '{row}' and column '{column}'.");
        }

        return element.Value;
    }

    private int FindConstraint(string axis, IValueComparer valueComparer, string dataTypeName, string value)
    {
        var constraint = _constraints.OrderBy(item => item.Index).FirstOrDefault(item =>
            item.Axis.Equals(axis, StringComparison.InvariantCultureIgnoreCase)
            &&
            valueComparer.IsSatisfiedBy(dataTypeName, item.Value, item.Comparison, value)
        );

        if (constraint == null)
        {
            throw new InvalidOperationException(
                $"There is no {axis.ToLowerInvariant()} constraint in matrix '{Name}' where argument '{RowArgumentId}' is satisfied by '{value}'.");
        }

        return constraint.Index;
    }

    public class Constraint
    {
        public Constraint(Guid id, string axis, int index, string comparison, string value)
        {
            if (!axis.Equals("Row", StringComparison.InvariantCultureIgnoreCase)
                &&
                !axis.Equals("Column", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new DomainException("Axis may only be 'Row' or 'Column'.");
            }

            Id = id;
            Axis = axis;
            Index = index;
            Comparison = comparison;
            Value = value;
        }

        public Guid Id { get; }
        public string Axis { get; }
        public int Index { get; }
        public string Comparison { get; }
        public string Value { get; }
    }

    public class Element(Guid id, int row, int column, string value)
    {
        public Guid Id { get; } = id;
        public int Row { get; } = row;
        public int Column { get; } = column;
        public string Value { get; } = value;
    }
}
