using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using Conectare_la_baza_de_date;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
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

namespace Irimies_Mircea_Proiect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        Entitatiile_bazei_mele_de_date ctx = new Entitatiile_bazei_mele_de_date();
        CollectionViewSource utilizatorViewSource;
        CollectionViewSource alegeri_utilizatoriViewSource;
        CollectionViewSource filozofieViewSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            utilizatorViewSource =
    ((System.Windows.Data.CollectionViewSource)(this.FindResource("utilizatorViewSource")));
            filozofieViewSource =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("filozofieViewSource")));
            utilizatorViewSource.Source = ctx.Utilizators.Local;
             filozofieViewSource.Source = ctx.Filozofies.Local;
            alegeri_utilizatoriViewSource =
            ((System.Windows.Data.CollectionViewSource)(this.FindResource("alegeri_utilizatoriViewSource")));
            alegeri_utilizatoriViewSource.Source = ctx.Alegeri_utilizatori.Local;
           
            BindDataGrid();
            ctx.Utilizators.Load();
            ctx.Filozofies.Load();
            ctx.Alegeri_utilizatori.Load();
            cmbUtilizatorii.ItemsSource = ctx.Utilizators.Local;
            cmbFilozofii.ItemsSource = ctx.Filozofies.Local;
            cmbFilozofii.SelectedValuePath = "Filozofie_Id";
            cmbUtilizatorii.SelectedValuePath = "Utilizator_Id";
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Utilizator utilizator = null;
            if (action == ActionState.New)
            {
                try
                {
                   
                    utilizator = new Utilizator()
                    {
                        Nume = numeTextBox.Text.Trim(),
                        Prenume = prenumeTextBox.Text.Trim()
                    };
                    
                    ctx.Utilizators.Add(utilizator);
                    utilizatorViewSource.View.Refresh();
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnDelete.IsEnabled = true;
                btnCancel.IsEnabled = false;
                btnPrev.IsEnabled = true;
                btnNext.IsEnabled = true;
                numeTextBox.IsEnabled = false;
                prenumeTextBox.IsEnabled = false;
                SetValidationBinding();

            }

            else if (action == ActionState.Edit)
            {
                try
                {
                    utilizator = (Utilizator)utilizatorDataGrid.SelectedItem;
                    utilizator.Nume = numeTextBox.Text.Trim();
                    utilizator.Prenume = prenumeTextBox.Text.Trim();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                utilizatorViewSource.View.Refresh();
                // pozitionarea pe item-ul curent
                utilizatorViewSource.View.MoveCurrentTo(utilizator);
                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;

                btnPrev.IsEnabled = true;
                btnNext.IsEnabled = true;
                numeTextBox.IsEnabled = false;
                prenumeTextBox.IsEnabled = false;
                SetValidationBinding();

            }

            else if (action == ActionState.Delete)
            {
                try
                {
                    utilizator = (Utilizator)utilizatorDataGrid.SelectedItem;
                    ctx.Utilizators.Remove(utilizator);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                utilizatorViewSource.View.Refresh();
                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;

                btnPrev.IsEnabled = true;
                btnNext.IsEnabled = true;
                numeTextBox.IsEnabled = false;
                prenumeTextBox.IsEnabled = false;
            }
            SetValidationBinding();
        }
        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            utilizatorViewSource.View.MoveCurrentToPrevious();

        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            utilizatorViewSource.View.MoveCurrentToNext();

        }




        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;

            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;

            btnPrev.IsEnabled = false;
            btnNext.IsEnabled = false;
            numeTextBox.IsEnabled = true;
            prenumeTextBox.IsEnabled = true;

            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);

            numeTextBox.Text = "";
            prenumeTextBox.Text = "";

            Keyboard.Focus(numeTextBox);
            SetValidationBinding();

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;


            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;

            btnPrev.IsEnabled = false;
            btnNext.IsEnabled = false;
            numeTextBox.IsEnabled = true;
            prenumeTextBox.IsEnabled = true;

            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);

            Keyboard.Focus(numeTextBox);
            SetValidationBinding();


        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;


            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;

            btnPrev.IsEnabled = false;
            btnNext.IsEnabled = false;
            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);

        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnNew.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;

            btnPrev.IsEnabled = true;
            btnNext.IsEnabled = true;
            numeTextBox.IsEnabled = false;
            prenumeTextBox.IsEnabled = false;

        }
        private void btnSave1_Click(object sender, RoutedEventArgs e)
        {
            Filozofie filozofie = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Customer entity
                    filozofie = new Filozofie()
                    {
                        Denumire = denumireTextBox.Text.Trim(),
                        Fondator = fondatorTextBox.Text.Trim(),
                        Perioada = perioadaTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Filozofies.Add(filozofie);
                    filozofieViewSource.View.Refresh();
                    //salvam modificarile
                    //ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                btnNew1.IsEnabled = true;
                btnEdit1.IsEnabled = true;
                btnSave1.IsEnabled = false;
                btnDelete1.IsEnabled = true;
                btnCancel1.IsEnabled = false;
                btnPrev1.IsEnabled = true;
                btnNext1.IsEnabled = true;
                denumireTextBox.IsEnabled = false;
                fondatorTextBox.IsEnabled = false;
                perioadaTextBox.IsEnabled = false;
            }

            else if (action == ActionState.Edit)
            {
                try
                {
                    filozofie = (Filozofie)filozofieDataGrid.SelectedItem;
                    filozofie.Denumire = denumireTextBox.Text.Trim();
                    filozofie.Fondator = fondatorTextBox.Text.Trim();
                    filozofie.Perioada = perioadaTextBox.Text.Trim();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                filozofieViewSource.View.Refresh();
                // pozitionarea pe item-ul curent
                filozofieViewSource.View.MoveCurrentTo(filozofie);
                btnNew1.IsEnabled = true;
                btnEdit1.IsEnabled = true;
                btnDelete1.IsEnabled = true;
                btnSave1.IsEnabled = false;
                btnCancel1.IsEnabled = false;

                btnPrev1.IsEnabled = true;
                btnNext1.IsEnabled = true;
                denumireTextBox.IsEnabled = false;
                fondatorTextBox.IsEnabled = false;
                perioadaTextBox.IsEnabled = false;
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    filozofie = (Filozofie)filozofieDataGrid.SelectedItem;
                    ctx.Filozofies.Remove(filozofie);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                filozofieViewSource.View.Refresh();
                btnNew1.IsEnabled = true;
                btnEdit1.IsEnabled = true;
                btnDelete1.IsEnabled = true;
                btnSave1.IsEnabled = false;
                btnCancel1.IsEnabled = false;

                btnPrev1.IsEnabled = true;
                btnNext1.IsEnabled = true;
                denumireTextBox.IsEnabled = false;
                fondatorTextBox.IsEnabled = false;
                perioadaTextBox.IsEnabled = false;


            }
            SetValidationBinding();

        }
        private void btnNew1_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnNew1.IsEnabled = false;
            btnEdit1.IsEnabled = false;
            btnDelete1.IsEnabled = false;

            btnSave1.IsEnabled = true;
            btnCancel1.IsEnabled = true;

            btnPrev1.IsEnabled = false;
            btnNext1.IsEnabled = false;
            denumireTextBox.IsEnabled = true;
            fondatorTextBox.IsEnabled = true;
            perioadaTextBox.IsEnabled = true;

            BindingOperations.ClearBinding(denumireTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(fondatorTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(perioadaTextBox, TextBox.TextProperty);
            denumireTextBox.Text = "";
            fondatorTextBox.Text = "";
            perioadaTextBox.Text = "";

            Keyboard.Focus(denumireTextBox);
        }
        private void btnEdit1_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;


            btnNew1.IsEnabled = false;
            btnEdit1.IsEnabled = false;
            btnDelete1.IsEnabled = false;
            btnSave1.IsEnabled = true;
            btnCancel1.IsEnabled = true;

            btnPrev1.IsEnabled = false;
            btnNext1.IsEnabled = false;
            denumireTextBox.IsEnabled = true;
            fondatorTextBox.IsEnabled = true;
            perioadaTextBox.IsEnabled = true;


            BindingOperations.ClearBinding(denumireTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(fondatorTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(perioadaTextBox, TextBox.TextProperty);

            Keyboard.Focus(denumireTextBox);
            SetValidationBinding();

        }

        private void btnDelete1_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;


            btnNew1.IsEnabled = false;
            btnEdit1.IsEnabled = false;
            btnDelete1.IsEnabled = false;
            btnSave1.IsEnabled = true;
            btnCancel1.IsEnabled = true;

            btnPrev1.IsEnabled = false;
            btnNext1.IsEnabled = false;
            BindingOperations.ClearBinding(denumireTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(fondatorTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(perioadaTextBox, TextBox.TextProperty);


        }

        private void btnCancel1_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnNew1.IsEnabled = true;
            btnEdit1.IsEnabled = true;
            btnEdit1.IsEnabled = true;
            btnSave1.IsEnabled = false;
            btnCancel1.IsEnabled = false;

            btnPrev1.IsEnabled = true;
            btnNext1.IsEnabled = true;
            denumireTextBox.IsEnabled = false;
            fondatorTextBox.IsEnabled = false;
            perioadaTextBox.IsEnabled = false;



        }



        private void btnPrev1_Click(object sender, RoutedEventArgs e)
        {
            filozofieViewSource.View.MoveCurrentToPrevious();

        }

        private void btnNext1_Click(object sender, RoutedEventArgs e)
        {
            filozofieViewSource.View.MoveCurrentToNext();


        }
        private void btnSave2_Click(object sender, RoutedEventArgs e)
        {
            Alegeri_utilizatori alegeri_utilizatori = null;
            if (action == ActionState.New)
            {
                try
                {
                    Utilizator utilizator = (Utilizator)cmbUtilizatorii.SelectedItem;
                    Filozofie filozofie = (Filozofie)cmbFilozofii.SelectedItem;
                    //instantiem Order entity
                   alegeri_utilizatori = new Alegeri_utilizatori()
                    {

                        Utilizator_Id = utilizator.Utilizator_Id,
                        Filozofie_Id = filozofie.Filozofie_Id
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Alegeri_utilizatori.Add(alegeri_utilizatori);
                    ctx.SaveChanges();
                    BindDataGrid();
                    alegeri_utilizatoriViewSource.View.Refresh();
                    //salvam modificarile

                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                btnNew2.IsEnabled = true;
                btnEdit2.IsEnabled = true;
                btnSave2.IsEnabled = false;
                btnDelete2.IsEnabled = true;
                btnCancel2.IsEnabled = false;
                btnPrev2.IsEnabled = true;
                btnNext2.IsEnabled = true;
                cmbUtilizatorii.IsEnabled = false;
                cmbFilozofii.IsEnabled = false;

            }
            else if (action == ActionState.Edit)
            {
                dynamic selectedAlegeri = alegeri_utilizatoriDataGrid.SelectedItem;
                try
                {
                    int curr_id = selectedAlegeri.Alegeri_Utilizatori_Id;
                    var editedAlegeri = ctx.Alegeri_utilizatori.FirstOrDefault(s => s.Alegeri_Utilizatori_Id == curr_id);
                    if (editedAlegeri != null)
                    {
                        editedAlegeri.Utilizator_Id = Int32.Parse(cmbUtilizatorii.SelectedValue.ToString());
                        editedAlegeri.Filozofie_Id = Convert.ToInt32(cmbFilozofii.SelectedValue.ToString());
                        //salvam modificarile
                        ctx.SaveChanges();

                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                BindDataGrid();
                // pozitionarea pe item-ul curent
                alegeri_utilizatoriViewSource.View.Refresh();
                utilizatorViewSource.View.MoveCurrentTo(selectedAlegeri);

                btnNew2.IsEnabled = true;
                btnEdit2.IsEnabled = true;
                btnDelete2.IsEnabled = true;
                btnSave2.IsEnabled = false;
                btnCancel2.IsEnabled = false;

                btnPrev2.IsEnabled = true;
                btnNext2.IsEnabled = true;
                cmbUtilizatorii.IsEnabled = false;
                cmbFilozofii.IsEnabled = false;
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    dynamic selectedAlegeri = alegeri_utilizatoriDataGrid.SelectedItem;
                    int curr_id = selectedAlegeri.Alegeri_Utilizatori_Id;
                    var deletedAlegeri = ctx.Alegeri_utilizatori.FirstOrDefault(s => s.Alegeri_Utilizatori_Id == curr_id);
                    if (deletedAlegeri != null)
                    {
                        ctx.Alegeri_utilizatori.Remove(deletedAlegeri);
                        ctx.SaveChanges();
                        MessageBox.Show("Alegere stearsa Successfully", "Message");
                        BindDataGrid();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                alegeri_utilizatoriViewSource.View.Refresh();
                btnNew2.IsEnabled = true;
                btnEdit2.IsEnabled = true;
                btnDelete2.IsEnabled = true;
                btnSave2.IsEnabled = false;
                btnCancel2.IsEnabled = false;
                btnPrev2.IsEnabled = true;
                btnNext2.IsEnabled = true;
                cmbUtilizatorii.IsEnabled = false;
                cmbFilozofii.IsEnabled = false;
            }

        }

        private void btnNew2_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnNew2.IsEnabled = false;
            btnEdit2.IsEnabled = false;
            btnDelete2.IsEnabled = false;

            btnSave2.IsEnabled = true;
            btnCancel2.IsEnabled = true;

            btnPrev2.IsEnabled = false;
            btnNext2.IsEnabled = false;
            cmbUtilizatorii.IsEnabled = true;
            cmbFilozofii.IsEnabled = true;

            BindingOperations.ClearBinding(cmbUtilizatorii, TextBox.TextProperty);
            BindingOperations.ClearBinding(cmbFilozofii, TextBox.TextProperty);


            Keyboard.Focus(cmbUtilizatorii);

        }

        private void btnEdit2_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            btnNew2.IsEnabled = false;
            btnEdit2.IsEnabled = false;
            btnDelete2.IsEnabled = false;
            btnSave2.IsEnabled = true;
            btnCancel2.IsEnabled = true;

            btnPrev2.IsEnabled = false;
            btnNext2.IsEnabled = false;
            cmbUtilizatorii.IsEnabled = true;
            cmbFilozofii.IsEnabled = true;

            BindingOperations.ClearBinding(cmbUtilizatorii, TextBox.TextProperty);
            BindingOperations.ClearBinding(cmbFilozofii, TextBox.TextProperty);


            Keyboard.Focus(cmbUtilizatorii);


        }

        private void btnDelete2_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;


            btnNew2.IsEnabled = false;
            btnEdit2.IsEnabled = false;
            btnDelete2.IsEnabled = false;
            btnSave2.IsEnabled = true;
            btnCancel2.IsEnabled = true;

            btnPrev2.IsEnabled = false;
            btnNext2.IsEnabled = false;
            BindingOperations.ClearBinding(cmbUtilizatorii, TextBox.TextProperty);
            BindingOperations.ClearBinding(cmbFilozofii, TextBox.TextProperty);


        }

        private void btnCancel2_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnNew2.IsEnabled = true;
            btnEdit2.IsEnabled = true;
            btnDelete2.IsEnabled = true;
            btnSave2.IsEnabled = false;
            btnCancel2.IsEnabled = false;

            btnPrev2.IsEnabled = true;
            btnNext2.IsEnabled = true;
            cmbUtilizatorii.IsEnabled = false;
            cmbFilozofii.IsEnabled = false;

        }

        private void btnPrev2_Click(object sender, RoutedEventArgs e)
        {
            alegeri_utilizatoriViewSource.View.MoveCurrentToPrevious();

        }

        private void btnNext2_Click(object sender, RoutedEventArgs e)
        {
            alegeri_utilizatoriViewSource.View.MoveCurrentToNext();

        }
        private void BindDataGrid()
        {
            var queryAlegeri = from ale in ctx.Alegeri_utilizatori
                             join uti in ctx.Utilizators on ale.Utilizator_Id equals
                             uti.Utilizator_Id
                             join filo in ctx.Filozofies on ale.Filozofie_Id
                 equals filo.Filozofie_Id
                             select new
                             {
                                 ale.Alegeri_Utilizatori_Id,
                                 ale.Utilizator_Id,
                                 ale.Filozofie_Id,
                                 uti.Nume,
                                 uti.Prenume,
                                 filo.Denumire,
                                 filo.Fondator,
                                 filo.Perioada
                             };
            alegeri_utilizatoriViewSource.Source = queryAlegeri.ToList();
        }
        private void SetValidationBinding()
        {

        }
    }
}
