using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.MethodTest.Results
{
    public partial class TestResultView : GenericMethodTestResultView, ITestResultView
    {
        public TestResultView()
        {
            InitializeComponent();

            ImageList.Images.Add("Success", Resources.Image_Mark);
            ImageList.Images.Add("Error", Resources.Image_Error);
        }

        protected ListViewItem SelectedItem
        {
            get { return MethodTestListView.SelectedItems[0]; }
        }


        public bool HasSelectedItem
        {
            get { return MethodTestListView.SelectedItems.Count > 0; }
        }

        public void ShowCalculationLog()
        {
            throw new NotImplementedException();
            //CalculationLog.Text = SelectedDTO.GetLog();
        }

        public void BuildDisplayTree(string name, List<GraphNodeDTO> items)
        {
            var root = DisplayTree.Nodes.Add(name);

            root.Tag = items;

            items.ForEach(item =>
                              {
                                  if (item.GraphNodes.Count > 0)
                                  {
                                      BuildDisplayTree(root, item.Name, item.GraphNodes);
                                  }
                              });
        }

        public void ClearDisplayList()
        {
            DisplayList.Items.Clear();
        }

        public bool HasSelectedGraphNode
        {
            get { return DisplayTree.SelectedNode != null; }
        }

        public List<GraphNodeDTO> SelectedGraphNodes()
        {
            return (List<GraphNodeDTO>) DisplayTree.SelectedNode.Tag;
        }

        public void AddGraphNode(GraphNodeDTO dto)
        {
            var item = DisplayList.Items.Add(dto.Name);

            var text = string.Empty;

            dto.GraphNodeArguments.ForEach(argumentDto => text += (text.Length > 0
                                                                               ? ", "
                                                                               : string.Empty) + argumentDto.Display);

            item.SubItems.Add(text);
            item.SubItems.Add(dto.Total.ToString(Resources.FormatDecimal));
            item.SubItems.Add(dto.SubTotal.ToString(Resources.FormatDecimal));
        }

        public void AddRun(Guid id, string description, string expectedResult)
        {
            throw new NotImplementedException();
            //Invoke(() =>
            //           {
            //               Remove(id);

            //               var ok = expectedResult.Equals(contextDTO.Total);

            //               var errorFree = contextDTO.ErrorMessages.Count == 0;

            //               var item = MethodTestListView.Items.Add(id.ToString(), description, ok && errorFree
            //                                                                                       ? "Success"
            //                                                                                       : "Error");

            //               if (ok && errorFree)
            //               {
            //                   item.SubItems.Add("Success");
            //                   item.SubItems[1].ForeColor = Color.Green;
            //               }
            //               else
            //               {
            //                   item.SubItems.Add(errorFree
            //                                         ? string.Format("Expected monthy total of {0} but was {1}",
            //                                                         expectedResult,
            //                                                         contextDTO.Total)
            //                                         : "Error(s)");
            //                   item.SubItems[1].ForeColor = Color.Red;
            //               }

            //               item.Tag = contextDTO;
            //           });
        }

        public void ClearResultDisplays()
        {
            CalculationLog.Text = string.Empty;
            DisplayTree.Nodes.Clear();

            ClearDisplayList();
        }

        private static void BuildDisplayTree(TreeNode owner, string name, List<GraphNodeDTO> items)
        {
            var node = owner.Nodes.Add(name);

            node.Tag = items;

            items.ForEach(item =>
                              {
                                  if (item.GraphNodes.Count > 0)
                                  {
                                      BuildDisplayTree(node, item.Name, item.GraphNodes);
                                  }
                              });
        }

        private void Remove(Guid id)
        {
            if (!MethodTestListView.Items.ContainsKey(id.ToString()))
            {
                return;
            }

            MethodTestListView.Items.RemoveByKey(id.ToString());
        }

        private void MethodTestListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Presenter.MethodTestResultSelected();
        }

        private void DisplayTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Presenter.GraphNodeSelected();
        }
    }

    public class GenericMethodTestResultView : View<ITestResultPresenter>
    {
    }
}