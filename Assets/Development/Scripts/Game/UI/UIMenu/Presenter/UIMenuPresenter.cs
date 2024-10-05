using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuPresenter 
{
    protected readonly UIMenuView _view;
    protected readonly UIMenuModel _model;

    public UIMenuPresenter(UIMenuView view, UIMenuModel model)
    {
        _view = view;
        _model = model;
    }

    
}
