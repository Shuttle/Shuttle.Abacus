﻿using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Calculation
{
    public interface ICalculationPresenter : IPresenter
    {
        void CalculationNameExited();
        void TypeChanged();
    }
}
