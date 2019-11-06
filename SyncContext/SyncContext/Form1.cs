using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncContext
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void mToolStripButtonThreads_Click(object sender, EventArgs e)
        {
            // посмотрим id потока
            int id = Thread.CurrentThread.ManagedThreadId;
            Trace.WriteLine("mToolStripButtonThreads_Click thread: " + id);

            // захватим контекст синхронизации ассоциированный с этим
            // потоком (UI поток), и сохраним его в uiContext
            // отметье что этот контекст устанавливается в UI потоке
            // во время создания формы (вне зоны вашего контроля)
            // также отметье, что не каждый поток имеет контекст синхронизации связанный с ним.
            SynchronizationContext uiContext = SynchronizationContext.Current;

            // Создадим поток и зададим ему метод Run для исполнения
            Thread thread = new Thread(Run);

            // Запустим поток и установим ему контекст синхронизации,
            // таким образом этот поток сможет обновлять UI
            thread.Start(uiContext);
        }

        private void Run(object state)
        {
            // смотри id потока
            int id = Thread.CurrentThread.ManagedThreadId;
            Trace.WriteLine("Run thread: " + id);

            // вытащим контекст синхронизации из state'а
            SynchronizationContext uiContext = state as SynchronizationContext;

            for (int i = 0; i < 1000; i++)
            {
                // Тут мог бы быть ваш код который обращается к базе
                // или выполняет какие-то вычисления
                Thread.Sleep(10);

                // испольуем UI контекст для обновления интерфейса, 
                // посредством исполнения метода UpdateUI, метод UpdateUI 
                // будет исполнен в UI потоке

                uiContext.Post(UpdateUI, "line " + i.ToString());
            }
        }

        /// <summary>
        /// Этот метод исполняется в основном UI потоке
        /// </summary>
        private void UpdateUI(object state)
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            Trace.WriteLine("UpdateUI thread:" + id);
            string text = state as string;
            mListBox.Items.Add(text);
        }
    }
}
