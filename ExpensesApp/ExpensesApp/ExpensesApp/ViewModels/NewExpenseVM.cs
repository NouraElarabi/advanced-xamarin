﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ExpensesApp.Models;
using Xamarin.Forms;

namespace ExpensesApp.ViewModels
{
    public class NewExpenseVM : INotifyPropertyChanged
    {
        private string expenseName;
        public string ExpenseName
        {
            get { return expenseName; }
            set
            {
                expenseName = value;
                OnPropertyChanged("ExpenseName");
            }
        }

        private string expenseDescription;
        public string ExpenseDescription
        {
            get { return expenseDescription; }
            set
            {
                expenseDescription = value;
                OnPropertyChanged("ExpenseDescription");
            }
        }

        private float expenseAmount;
        public float ExpenseAmount
        {
            get { return expenseAmount; }
            set
            {
                expenseAmount = value;
                OnPropertyChanged("ExpenseAmount");
            }
        }

        private DateTime expenseDate;
        public DateTime ExpenseDate
        {
            get { return expenseDate; }
            set
            {
                expenseDate = value;
                OnPropertyChanged("ExpenseDate");
            }
        }

        private string expenseCategory;
        public string ExpenseCategory
        {
            get { return expenseCategory; }
            set
            {
                expenseCategory = value;
                OnPropertyChanged("ExpenseCategory");
            }
        }

        public Command SaveExpenseCommand
        {
            get;
            set;
        }

        public ObservableCollection<string> Categories
        {
            get;
            set;
        }

        public ObservableCollection<ExpenseStatus> ExpenseStatuses
        {
            get;
            set;
        }

        public NewExpenseVM()
        {
            Categories = new ObservableCollection<string>();
            ExpenseStatuses = new ObservableCollection<ExpenseStatus>();
            ExpenseDate = DateTime.Today;
            SaveExpenseCommand = new Command(InsertExpense);
            GetCategories();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void InsertExpense()
        {
            var vm = this;
            Expense expense = new Expense()
            {
                Name = ExpenseName,
                Amount = ExpenseAmount,
                Category = ExpenseCategory,//after adding localization this will cause an issu, it should have been using an ID or something not text
                Date = ExpenseDate,
                Description = ExpenseDescription
            };

            int response = Expense.InsertExpense(expense);

            if (response > 0)
                Application.Current.MainPage.Navigation.PopAsync();
            else
                Application.Current.MainPage.DisplayAlert("Error", "No items were inserter", "Ok");
        }

        private void GetCategories()
        {
            Categories.Clear();
            //Categories.Add("Housing");
            //Categories.Add("Debt");
            //Categories.Add("Health");
            //Categories.Add("Food");
            //Categories.Add("Personal");
            //Categories.Add("Travel");
            //Categories.Add("Other");
            Categories.Add(ExpensesApp.Resources.Resources.housingCategory);
            Categories.Add(ExpensesApp.Resources.Resources.debtCategory);
            Categories.Add(ExpensesApp.Resources.Resources.healthCategory);
            Categories.Add(ExpensesApp.Resources.Resources.foodCategory);
            Categories.Add(ExpensesApp.Resources.Resources.personalCategory);
            Categories.Add(ExpensesApp.Resources.Resources.travelCategory);
            Categories.Add(ExpensesApp.Resources.Resources.otherCategory);
        }

        public void GetExpenseStatus()
        {
            ExpenseStatuses.Clear();
            ExpenseStatuses.Add(new ExpenseStatus()
            {
                Name = "Random",
                Status = true
            });
            ExpenseStatuses.Add(new ExpenseStatus()
            {
                Name = "Random 2",
                Status = true
            });
            ExpenseStatuses.Add(new ExpenseStatus()
            {
                Name = "Random 3",
                Status = false
            });
        }

        public class ExpenseStatus
        {
            public string Name
            {
                get;
                set;
            }

            public bool Status
            {
                get;
                set;
            }
        }
    }
}
