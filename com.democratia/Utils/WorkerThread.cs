using System.ComponentModel;
using Microsoft.Maui.Controls;

namespace com.democratia.Utils
{
    /// <summary>
    /// Cette classe emballe un wrapper autour de la classe BackgroundWorker, permettant l'exécution facile de méthodes dans un thread séparé.
    /// </summary>
    public sealed class WorkerThread
    {
        private Delegate @delegate;
        private Object?[]? parameters;
        private bool cancellation;
        private Action<object>? onFinish;
        private Action? onCanel;
        public BackgroundWorker _backgroundWorker { get; private set; }
        private bool runNow;


        /// <summary>
        /// Constructeur de la classe WorkerThread pour une fonction avec paramètres.
        /// </summary>
        /// <param name="delegate"> référence de la fonction a executé en arrière plan</param>
        /// <param name="cancellation">si vrai, donner la fonction onCancel afin d'executer une tâche durant la progression</param>
        /// <param name="onFinish">fonction à executer qui prend en paramètre, le résultat de la fonction en arrière plan</param>
        /// <param name="onCancel">fonction a executer en cas d'annulation, sans paramètre et ne retourne rien</param>
        /// <param name="runNow">si vrai, la fonction sera executer dès l'instanciation, sinon, après l'appel de _backgroundWorker.RunWorkerAsync() en ne mettant aucun paramètre, car déjà récupéré via le constructeur</param>
        /// <param name="parameters">les paramètres de la fonction</param>
        /// <summary>
        public WorkerThread(Delegate @delegate,bool cancellation, Action<object>? onFinish, Action? onCancel, bool runNow, params Object[] parameters) 
        {
            this.@delegate = @delegate;
            this.parameters = parameters;
            this.cancellation = cancellation;
            if (this.cancellation && onCancel!=null) this.onCanel = onCancel;
            this.onFinish = onFinish; 
            this.runNow = runNow;
            _backgroundWorker = new BackgroundWorker();
            RunWorkerThread();
        }
        /// <summary>
        /// Constructeur de la classe WorkerThread pour une fonction sans paramètres.
        /// </summary>
        /// <param name="delegate"> référence de la fonction a executé en arrière plan</param>
        /// <param name="cancellation">si vrai, donner la fonction onCancel afin d'executer une tâche durant la progression</param>
        /// <param name="onFinish">fonction à executer qui prend en paramètre, le résultat de la fonction en arrière plan</param>
        /// <param name="onCancel">fonction a executer en cas d'annulation, sans paramètre et ne retourne rien</param>
        /// <param name="runNow">si vrai, la fonction sera executer dès l'instanciation, sinon, après l'appel de _backgroundWorker.RunWorkerAsync() en ne mettant aucun paramètre, car déjà récupéré via le constructeur</param>
        public WorkerThread(Delegate @delegate, Action<object>? onFinish, Action? onCancel, bool cancellation, bool runNow)
        {
            this.@delegate = @delegate;
            this.cancellation = cancellation;
            if (this.cancellation && onCancel != null) this.onCanel = onCancel; 
            this.onFinish = onFinish;
            this.runNow = runNow;
            _backgroundWorker = new BackgroundWorker();
            RunWorkerThread();
        }

        private void RunWorkerThread()
        {
            _backgroundWorker.WorkerReportsProgress = false;
            _backgroundWorker.WorkerSupportsCancellation = cancellation;
            
            _backgroundWorker.DoWork += backgroundWorker_doWork;
            _backgroundWorker.RunWorkerCompleted += backgroundWorker_workerCompleted;
            if (!_backgroundWorker.IsBusy && runNow) _backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_workerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null) throw e.Error;
            else if (e.Cancelled) onCanel?.Invoke();
            else if (e.Result != null) onFinish?.Invoke(e.Result);
        }

        private void backgroundWorker_doWork(object? sender, DoWorkEventArgs e)
        {
            BackgroundWorker? worker = sender as BackgroundWorker;
            
            if (worker != null && worker.CancellationPending && worker.WorkerSupportsCancellation) e.Cancel = true;

            if (IsFuncType()) e.Result = DynamicInvokeSmart(parameters);
            else DynamicInvokeSmart(parameters);

        }
        private bool IsFuncType()
        {
            var type = @delegate.GetType();
            return type.IsGenericType && type.GetGenericTypeDefinition().Name.StartsWith("Func");
        }
        private object? DynamicInvokeSmart(object?[]? flatArgs)
        {
            var parameters = @delegate.Method.GetParameters();

            if (parameters.Length == 0)
                return @delegate.DynamicInvoke();

            var args = new object?[parameters.Length];

            bool hasParams = parameters.Last().IsDefined(typeof(ParamArrayAttribute), false);
            int fixedCount = hasParams ? parameters.Length - 1 : parameters.Length;

            // Vérifier qu'on a assez d'arguments
            if (flatArgs?.Length < fixedCount)
                throw new ArgumentException("Pas assez d’arguments pour les paramètres non-params");

            // Copier les arguments fixes
            for (int i = 0; i < fixedCount; i++)
                args[i] = flatArgs?[i];

            if (hasParams)
            {
                var paramElementType = parameters.Last().ParameterType.GetElementType()!;
                var paramArgs = flatArgs?.Skip(fixedCount).ToArray();

                var paramArray = Array.CreateInstance(paramElementType, paramArgs.Length);
                for (int i = 0; i < paramArgs.Length; i++)
                    paramArray.SetValue(paramArgs[i], i);

                args[parameters.Length - 1] = paramArray;
            }

            return @delegate.DynamicInvoke(args);
        }


    }
}
