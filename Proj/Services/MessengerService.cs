using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Proj.Services
{//TODO Dokoncz messengera, tzn dorob kolejne metody ..
    static class MessengerService
    {
        public static Result ShowMessageBox(string message, string caption, MessageType messageType, MessageIcon messageIcon)
        {
            MessageBoxButton messageButton = MessageBoxButton.OK;
            switch (messageType)
            {
                case MessageType.OK:
                    messageButton = MessageBoxButton.OK;
                    break;
                case MessageType.OkCancel:
                    messageButton = MessageBoxButton.OKCancel;
                    break;
                case MessageType.YesNo:
                    messageButton = MessageBoxButton.YesNo;
                    break;
                case MessageType.YesNoCancel:
                    messageButton = MessageBoxButton.YesNoCancel;
                    break;
                default:
                    break;
            }

            MessageBoxImage messageImage = MessageBoxImage.Information;
            switch (messageIcon)
            {
                case MessageIcon.Information:
                    messageImage = MessageBoxImage.Information;
                    break;
                case MessageIcon.Warning:
                    messageImage = MessageBoxImage.Warning;
                    break;
                case MessageIcon.Error:
                    messageImage = MessageBoxImage.Error;
                    break;
                case MessageIcon.Question:
                    messageImage = MessageBoxImage.Question;
                    break;
                default:
                    break;
            }

            switch (MessageBox.Show(message, caption, messageButton, messageImage))
            {
                case MessageBoxResult.OK:
                    return Result.Ok;
                case MessageBoxResult.Cancel:
                    return Result.Cancel;
                case MessageBoxResult.Yes:
                    return Result.Yes;
                case MessageBoxResult.No:
                    return Result.No;
                case MessageBoxResult.None:
                    return Result.None;
                default:
                    return Result.None;
            }
        }

        public enum MessageType
        { 
            OK,
            OkCancel,
            YesNo,
            YesNoCancel
        }

        public enum MessageIcon
        { 
            Information,
            Warning,
            Error,
            Question
        }

        public enum Result
        { 
            Ok,
            Cancel,
            Yes,
            No,
            None
        }
    }
}
