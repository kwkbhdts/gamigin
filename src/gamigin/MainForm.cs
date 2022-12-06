namespace Gamigin
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            gamiginApp = GamiginApp.Instance;
            gamiginApp.MainFormInstance = this;
            InitializeComponent();
        }

        /// <summary>
        /// updateTargetFilePathをUIスレッド外から起動する
        /// </summary>
        /// <param name="targetFilePath"></param>
        public void InvokeUpdateTargetFilePath(string targetFilePath)
        {
            Invoke((string x) => updateTargetFilePath(x), targetFilePath);
        }

        /// <summary>
        /// endMonitoringをUIスレッド外から起動する
        /// </summary>
        public void InvokeEndMonitoring()
        {
            Invoke(() => endMonitoring());
        }

        /// <summary>
        /// 開くボタン押下イベント
        /// </summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント</param>
        private void OnOpenButtonClicked(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "対象ファイルを選択してください";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                updateTargetFilePath(ofd.FileName);
            }

            // ファイルを指定したら自動的に監視を始める
            startMonitoring();
        }

        /// <summary>
        /// 開始/終了ボタン押下イベント
        /// </summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント</param>
        private void OnStartAndEndButtonClicked(object sender, EventArgs e)
        {
            if (gamiginApp.IsMonitoring)
            {
                endMonitoring();
            }
            else
            {
                startMonitoring();
            }
        }

        /// <summary>
        /// 対象ファイルパスを更新する
        /// </summary>
        /// <param name="targetFilePath">対象ファイルパス</param>
        private void updateTargetFilePath(string targetFilePath)
        {
            gamiginApp.TargetFilePath = targetFilePath;
            targetFilePathTextBox.Text = targetFilePath;
            targetFilePathTextBox.Select(targetFilePath.Length, 0);
        }

        /// <summary>
        /// 監視を開始する
        /// </summary>
        private void startMonitoring()
        {
            // 開くボタンを無効化
            openButton.Enabled = false;

            // ドラッグ＆ドロップをグレーアウト
            dragAndDropLabel.Enabled = false;
            dragAndDropPictureBox.Enabled = false;

            // 終了ボタンに変更する
            startAndEndButton.Text = END_BUTTON_TEXT;
            startAndEndButton.Image = Properties.Resources.hourglass_red;

            // 監視を開始
            if (!gamiginApp.StartMonitoring())
            {
                endMonitoring();
            }
        }

        /// <summary>
        /// 監視を終了する
        /// </summary>
        private void endMonitoring()
        {
            // 開くボタンを有効化
            openButton.Enabled = true;

            // ドラッグ＆ドロップをグレーアウト解除
            dragAndDropLabel.Enabled = true;
            dragAndDropPictureBox.Enabled = true;

            // 開始ボタンに変更する
            startAndEndButton.Text = START_BUTTON_TEXT;
            startAndEndButton.Image = Properties.Resources.hourglass_blue;

            // 監視を終了
            gamiginApp.EndMonitoring();
        }

        /// <summary>
        /// ドラッグドロップイベント
        /// </summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント</param>
        private void OnDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data == null)
            {
                return;
            }

            // 監視中のドラッグ＆ドロップは無視する
            if (gamiginApp.IsMonitoring)
            {
                return;
            }

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (files.Length > 0)
            {
                updateTargetFilePath(files[0]);

                // ファイルを指定したら自動的に監視を始める
                startMonitoring();
            }
        }

        /// <summary>
        /// ドラッグエンターイベント
        /// </summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント</param>
        private void OnDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data == null)
            {
                return;
            }

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// GamiginAppインスタンス
        /// </summary>
        private GamiginApp gamiginApp;

        /// <summary>
        /// 開始ボタンのテキスト
        /// </summary>
        const string START_BUTTON_TEXT = "start";

        /// <summary>
        /// 終了ボタンのテキスト
        /// </summary>
        const string END_BUTTON_TEXT = "end";
    }
}