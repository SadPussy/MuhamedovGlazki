using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MuhamedovGlazki
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            var currentServices = MuhamedovGlazkiSaveEntities.GetContext().Agent.ToList();
            AgentListView.ItemsSource = currentServices;

            ComboType.SelectedIndex = 0;
            ComboAgentType.SelectedIndex = 0;

            
        }

        private void UpdateAgents()
        {
            var currentAgents = MuhamedovGlazkiSaveEntities.GetContext().Agent.ToList();

            currentAgents = currentAgents.Where(p => p.Title.ToLower().Contains(TBox_Search.Text.ToLower()) || p.Phone.Replace("-"," ").Replace("(","").Replace(")","").Replace(" ", "").Contains(TBox_Search.Text.ToLower()) || p.Email.ToLower().Contains(TBox_Search.Text.ToLower())).ToList();



            AgentListView.ItemsSource = currentAgents.ToList();

            if (ComboAgentType.SelectedIndex == 1)
            {
                currentAgents = currentAgents.Where(p => p.AgentType.Title == "МФО").ToList();
            }

            if (ComboAgentType.SelectedIndex == 2)
            {
                currentAgents = currentAgents.Where(p => p.AgentType.Title == "ООО").ToList();
            }

            if (ComboAgentType.SelectedIndex == 3)
            {
                currentAgents = currentAgents.Where(p => p.AgentType.Title == "ЗАО").ToList();
            }

            if (ComboAgentType.SelectedIndex == 4)
            {
                currentAgents = currentAgents.Where(p => p.AgentType.Title == "МКК").ToList();
            }

            if (ComboAgentType.SelectedIndex == 5)
            {
                currentAgents = currentAgents.Where(p => p.AgentType.Title == "ОАО").ToList();
            }

            if (ComboAgentType.SelectedIndex == 6)
            {
                currentAgents = currentAgents.Where(p => p.AgentType.Title == "ПАО").ToList();
            }
            AgentListView.ItemsSource = currentAgents.ToList();

            if (ComboType.SelectedIndex == 1)
            {
                AgentListView.ItemsSource = currentAgents.OrderBy(p => p.Title).ToList();
            }

            if (ComboType.SelectedIndex == 2)
            {
                AgentListView.ItemsSource = currentAgents.OrderByDescending(p => p.Title).ToList();
            }

            if (ComboType.SelectedIndex == 3)
            {
                AgentListView.ItemsSource = currentAgents.OrderBy(p => p.Priority).ToList();
            }

            if (ComboType.SelectedIndex == 4)
            {
                AgentListView.ItemsSource = currentAgents.OrderByDescending(p => p.Priority).ToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void TBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAgents();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();
        }


        private void ComboAgentType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();
        }
    }
}
