﻿namespace WasteApp.Core
{
    public interface INavigationService
    {
        void Push(PageEnum page, params object[] parameters);
        void Pop();
    }
}