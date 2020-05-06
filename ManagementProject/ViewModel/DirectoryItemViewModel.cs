﻿using ManagementProject.UserControls.FightControl.CamResource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ManagementProject.ViewModel
{
    public class DirectoryItemViewModel: DirectoryItemModel
    {
        #region Public Properties

        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        public string ImageName => Type == DirectoryItemType.Drive ? "drive" : (Type == DirectoryItemType.File ? "file" : (IsExpanded ? "folder-open" : "folder-closed"));

        /// <summary>
        /// The full path to the item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string Name => Type == DirectoryItemType.Drive ? FullPath : DirectoryStructure.GetFileFolderName(FullPath);

       

        /// <summary>
        /// Indicates if this item can be expanded
        /// </summary>
        public bool CanExpand => Type != DirectoryItemType.File;

        /// <summary>
        /// Indicates if the current item is expanded or not
        /// </summary>
        public bool IsExpanded
        {
            get => Children?.Count(f => f != null) > 0;
            set
            {
                // If the UI tells us to expand...
                if (value)
                    // Find all children
                    Expand(null);
                // If the UI tells us to close
                else
                    ClearChildren();
            }
        }

        #endregion

        #region Public Commands

        /// <summary>
        /// The command to expand this item
        /// </summary>
        public DelegateCommand ExpandCommand { get; set; }

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fullPath">The full path of this item</param>
        /// <param name="type">The type of item</param>
        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            // Create commands
            ExpandCommand =new DelegateCommand { ExecuteCommand=new Action<object>(Expand) };

            // Set path and type
            FullPath = fullPath;
            Type = type;

            // Setup the children as needed
            ClearChildren();
        }

        #region Helper Methods

        /// <summary>
        /// Removes all children from the list, adding a dummy item to show the expand icon if required
        /// </summary>
        private void ClearChildren()
        {
            // Clear items
            Children = new ObservableCollection<DirectoryItemViewModel>();

            // Show the expand arrow if we are not a file
            if (Type != DirectoryItemType.File)
                Children.Add(null);
        }

        #endregion

        /// <summary>
        ///  Expands this directory and finds all children
        /// </summary>
        private void Expand(object obj)
        {
            // We cannot expand a file
            if (Type == DirectoryItemType.File)
                return;

            // Find all children
            var children = DirectoryStructure.GetDirectoryContents(FullPath);
            Children = new ObservableCollection<DirectoryItemViewModel>(
                                children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
        }
    }

    public class DirectoryItemModel:INotifyPropertyChangedClass
    {

        private ObservableCollection<DirectoryItemViewModel> children;

        ///// <summary>
        ///// A list of all children contained inside this item
        ///// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children
        {
            get { return children; }
            set
            {
                children = value;
                NotifyPropertyChanged("Children");
            }
        }
    }
}