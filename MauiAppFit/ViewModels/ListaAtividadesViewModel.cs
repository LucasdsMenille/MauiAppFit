using MauiAppFit.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Security.Cryptography.X509Certificates;

namespace MauiAppFit.ViewModels
{
    internal class ListaAtividadesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /**
         * pegando o que foi digitado pelo usuário.
         */

        public string ParametroBusca { get; set; }

        /** 
         * Gerencia se mostra ao usuário o RefreshView.
        */
        bool estaAtualizando = false;

        public bool EstaAtualizando
        {
            get => estaAtualizando;
            set
            {
                estaAtualizando = value;
                PropertyChanged(this,
                    new PropertyChangedEventArgs("EstaAtualizando"));
            }
        }
                /** 
                     * Coleção que armazena as atividades do usuário.
                */
                ObservableCollection<Atividade> listaAtividades = new ObservableCollection<Atividade>();
                public ObservableCollection<Atividade> ListaAtividades
                { 
                    get => listaAtividades;
                    set => listaAtividades = value;
                }
        public ICommand AtualizarLista
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (EstaAtualizando)
                            return;
                        EstaAtualizando = true;

                        List<Atividade> tmp =
                        await App.Database.GetAllRows();

                        ListaAtividades.Clear();
                        tmp.ForEach(i => ListaAtividades.Add(i));
                    }
                    catch (Exception ex)
                    {
                        await Shell.Current.DisplayAlert(
                            "Ops", ex.Message, "OK");
                    }
                    finally
                    {
                        EstaAtualizando = false;
                    }

                });
            } //Fecha get
        } //Fecha AtualizarLista
    }
}