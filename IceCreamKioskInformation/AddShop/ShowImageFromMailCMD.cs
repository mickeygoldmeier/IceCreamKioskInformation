﻿using System;
using System.ComponentModel;
using System.Windows.Input;

namespace IceCreamKioskInformation.AddShop
{
    class ShowImageFromMailCMD : ICommand
    {
        private AddShopUserControlVM VM;
        private BackgroundWorker FetchImageBW;

        public ShowImageFromMailCMD(AddShopUserControlVM VM)
        {
            this.VM = VM;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VM.LookingForImage();
            FetchImageBW = new BackgroundWorker();
            FetchImageBW.WorkerSupportsCancellation = true;
            FetchImageBW.DoWork += FetchImage;
            FetchImageBW.RunWorkerCompleted += ImageFound;
            FetchImageBW.RunWorkerAsync();
        }

        private async void FetchImage(object sender, DoWorkEventArgs e)
        {
            AddShopUserControlM M = new AddShopUserControlM();
            try
            {
                VM.NewShop.ImageURL = await M.getImageFrom(VM.ImageTrys++);
                e.Result = true;
            }
            catch (Exception)
            {
                e.Result = false;
            }
        }

        private void ImageFound(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((bool)e.Result)
                VM.ImageFound();
            else
                VM.ImageNotFound();
        }
    }
}
