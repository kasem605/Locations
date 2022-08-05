using System;
using Foundation;
using Locations.Controls;
using Locations.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DatePickerEntryCell), typeof(DatePickerEntryCellRenderer))]
namespace Locations.iOS.Renderers
{
    public class DatePickerEntryCellRenderer : EntryCellRenderer
    {

        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            UITableViewCell cell = base.GetCell(item, reusableCell, tv);

            DatePickerEntryCell datepickerCell = (DatePickerEntryCell)item;

            UITextField textField = null;

            if(cell != null)
            {
                textField = (UITextField)cell.ContentView.Subviews[0];
            }

            //Default datepicker based on Cell's properties
            UIDatePickerMode mode = UIDatePickerMode.Date;
            string displayFormat = "d";
            NSDate date = NSDate.Now;
            bool isLocalTime = false;

            //Update datepicker based on cell's properties
            if(datepickerCell != null)
            {
                //Kind must be universal or local to cast to NSDate
                if(datepickerCell.Date.Kind == DateTimeKind.Unspecified)
                {
                    DateTime local = new DateTime(datepickerCell.Date.Ticks, DateTimeKind.Local);
                    date = (NSDate)local;
                }
                else
                {
                    date = (NSDate)datepickerCell.Date;
                }

                isLocalTime = datepickerCell.Date.Kind == DateTimeKind.Local || datepickerCell.Date.Kind == DateTimeKind.Unspecified;
            }

            //create IOS datepicker
            UIDatePicker datePicker = new UIDatePicker
            {
                Mode = mode,
                BackgroundColor = UIColor.White,
                Date = date,
                TimeZone = isLocalTime ? NSTimeZone.LocalTimeZone : new NSTimeZone("UTC")

            };

            //create a toolbar with a done button
            //that will close the datepicker and set the selected value

            UIBarButtonItem done = new UIBarButtonItem("Done", UIBarButtonItemStyle.Done, (s, e) =>
            {
                DateTime pickedDate = (DateTime)datePicker.Date;

                if (isLocalTime)
                {
                    pickedDate = pickedDate.ToLocalTime();
                }

                //update the value of the UITextField within the cell
                if(textField != null)
                {
                    textField.Text = pickedDate.ToString(displayFormat);
                    textField.ResignFirstResponder();
                }

                //update the Date property on the cell
                if(datepickerCell != null)
                {
                    datepickerCell.Date = pickedDate;
                    datepickerCell.SendCompleted();
                }
            });

            UIToolbar toolBar = new UIToolbar
            {
                BarStyle =UIBarStyle.Default,
                Translucent = false
            };

            toolBar.SizeToFit();
            toolBar.SetItems(new[] { done }, true);

            //set the input view, toolbar and initial value for the Cell's UITextField
            if(textField != null)
            {

                    textField.InputView = datePicker;
                    textField.InputAccessoryView = toolBar;

                if (datepickerCell != null)
                {
                    textField.Text = datepickerCell.Date.ToString(displayFormat);
                }                
            }

            return cell;
        }
        
    }
}

