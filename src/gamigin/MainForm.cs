namespace Gamigin
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public MainForm()
        {
            gamiginApp = GamiginApp.Instance;
            gamiginApp.MainFormInstance = this;
            InitializeComponent();
        }

        /// <summary>
        /// updateTargetFilePath��UI�X���b�h�O����N������
        /// </summary>
        /// <param name="targetFilePath"></param>
        public void InvokeUpdateTargetFilePath(string targetFilePath)
        {
            Invoke((string x) => updateTargetFilePath(x), targetFilePath);
        }

        /// <summary>
        /// endMonitoring��UI�X���b�h�O����N������
        /// </summary>
        public void InvokeEndMonitoring()
        {
            Invoke(() => endMonitoring());
        }

        /// <summary>
        /// �J���{�^�������C�x���g
        /// </summary>
        /// <param name="sender">�C�x���g������</param>
        /// <param name="e">�C�x���g</param>
        private void OnOpenButtonClicked(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "�Ώۃt�@�C����I�����Ă�������";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                updateTargetFilePath(ofd.FileName);
            }

            // �t�@�C�����w�肵���玩���I�ɊĎ����n�߂�
            startMonitoring();
        }

        /// <summary>
        /// �J�n/�I���{�^�������C�x���g
        /// </summary>
        /// <param name="sender">�C�x���g������</param>
        /// <param name="e">�C�x���g</param>
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
        /// �Ώۃt�@�C���p�X���X�V����
        /// </summary>
        /// <param name="targetFilePath">�Ώۃt�@�C���p�X</param>
        private void updateTargetFilePath(string targetFilePath)
        {
            gamiginApp.TargetFilePath = targetFilePath;
            targetFilePathTextBox.Text = targetFilePath;
            targetFilePathTextBox.Select(targetFilePath.Length, 0);
        }

        /// <summary>
        /// �Ď����J�n����
        /// </summary>
        private void startMonitoring()
        {
            // �J���{�^���𖳌���
            openButton.Enabled = false;

            // �h���b�O���h���b�v���O���[�A�E�g
            dragAndDropLabel.Enabled = false;
            dragAndDropPictureBox.Enabled = false;

            // �I���{�^���ɕύX����
            startAndEndButton.Text = END_BUTTON_TEXT;
            startAndEndButton.Image = Properties.Resources.hourglass_red;

            // �Ď����J�n
            if (!gamiginApp.StartMonitoring())
            {
                endMonitoring();
            }
        }

        /// <summary>
        /// �Ď����I������
        /// </summary>
        private void endMonitoring()
        {
            // �J���{�^����L����
            openButton.Enabled = true;

            // �h���b�O���h���b�v���O���[�A�E�g����
            dragAndDropLabel.Enabled = true;
            dragAndDropPictureBox.Enabled = true;

            // �J�n�{�^���ɕύX����
            startAndEndButton.Text = START_BUTTON_TEXT;
            startAndEndButton.Image = Properties.Resources.hourglass_blue;

            // �Ď����I��
            gamiginApp.EndMonitoring();
        }

        /// <summary>
        /// �h���b�O�h���b�v�C�x���g
        /// </summary>
        /// <param name="sender">�C�x���g������</param>
        /// <param name="e">�C�x���g</param>
        private void OnDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data == null)
            {
                return;
            }

            // �Ď����̃h���b�O���h���b�v�͖�������
            if (gamiginApp.IsMonitoring)
            {
                return;
            }

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (files.Length > 0)
            {
                updateTargetFilePath(files[0]);

                // �t�@�C�����w�肵���玩���I�ɊĎ����n�߂�
                startMonitoring();
            }
        }

        /// <summary>
        /// �h���b�O�G���^�[�C�x���g
        /// </summary>
        /// <param name="sender">�C�x���g������</param>
        /// <param name="e">�C�x���g</param>
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
        /// GamiginApp�C���X�^���X
        /// </summary>
        private GamiginApp gamiginApp;

        /// <summary>
        /// �J�n�{�^���̃e�L�X�g
        /// </summary>
        const string START_BUTTON_TEXT = "start";

        /// <summary>
        /// �I���{�^���̃e�L�X�g
        /// </summary>
        const string END_BUTTON_TEXT = "end";
    }
}