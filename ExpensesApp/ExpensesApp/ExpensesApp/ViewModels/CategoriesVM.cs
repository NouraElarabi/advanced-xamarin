using ExpensesApp.Interfaces;
using ExpensesApp.Models;
using PCLStorage;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace ExpensesApp.ViewModels
{
    public class CategoriesVM
    {
        public ObservableCollection<string> Categories
        {
            get;
            set;
        }

        public ObservableCollection<CategoryExpenses> CategoryExpensesCollection
        {
            get;
            set;
        }

        public Command ExportCommand
        {
            get;
            set;
        }

        public CategoriesVM()
        {
            ExportCommand = new Command(ShareReportAsync);
            Categories = new ObservableCollection<string>();
            CategoryExpensesCollection = new ObservableCollection<CategoryExpenses>();

            GetCategories();
            GetExpensesPerCategory();
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

        public void GetExpensesPerCategory()
        {
            CategoryExpensesCollection.Clear();

            float totalExpensesAmmount = Expense.TotalExpensesAmount();
            foreach (string c in Categories)
            {
                var expenses = Expense.GetExpenses(c);
                float expensesAmmountInCategory = expenses.Sum(e => e.Amount);

                CategoryExpenses ce = new CategoryExpenses()
                {
                    Category = c,
                    ExpensesPercentage = expensesAmmountInCategory / totalExpensesAmmount
                };

                CategoryExpensesCollection.Add(ce);
            }
        }

        public async void ShareReportAsync()
        {
            IFileSystem fileSystem = FileSystem.Current;
            IFolder rootFolder = fileSystem.LocalStorage;
            IFolder reportsFolder = await rootFolder.CreateFolderAsync("reports", CreationCollisionOption.OpenIfExists);

            var txtFile = await reportsFolder.CreateFileAsync("report.txt", CreationCollisionOption.ReplaceExisting);

            using (StreamWriter sw = new StreamWriter(txtFile.Path))
            {
                foreach (var ce in CategoryExpensesCollection)
                {
                    sw.WriteLine($"{ce.Category} - {ce.ExpensesPercentage:p}");
                }
            }

            IShare shareDependency = DependencyService.Get<IShare>();
            await shareDependency.Show("Expense Report", "Here is your expenses report", txtFile.Path);
        }

        public class CategoryExpenses
        {
            public string Category
            {
                get;
                set;
            }

            public float ExpensesPercentage
            {
                get;
                set;
            }
        }
    }
}