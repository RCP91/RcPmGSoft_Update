using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using Octokit;
using static AutoUpdate.Classes.JsonWork;

namespace AutoUpdate.Classes
{
    public partial class F_Update : Form
    {
        Version latestGitHubVersion;
        public static string? owner;
        public static string? repo;
        public static string? downloadUrl;
        public static string? version;

        // extractPath e jsonFolderPath são resultando do diretório a uma pasta a cima.
        public static string? extractPath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
        public static string? jsonFolderPath = Path.Combine(extractPath, "..\\");

        public F_Update()
        {
            LoadJson();
            InitializeComponent();
            lb_txt.Text = "Versão Atual: " + version + "\n Checking Update...";
            progressBar1.Visible = false;
            _ = CheckForUpdate();
        }

        private async Task CheckForUpdate()
        {
            try
            {
                await Task.Delay(3000);
                // Obter todas as releases do GitHub
                GitHubClient client = new GitHubClient(new ProductHeaderValue("update.zip"));
                IReadOnlyList<Release> releases = await client.Repository.Release.GetAll(owner, repo);

                // Configurar as versões
                latestGitHubVersion = new Version(releases[0].Name);

                if (latestGitHubVersion.ToString() != version)
                {
                    lb_txt.Text = $"Nova versão disponível: {latestGitHubVersion}";
                    await Task.Delay(3000);
                    // Obter o URL de download para a última versão
                    // Presumindo que o primeiro ativo é o arquivo ZIP
                    downloadUrl = releases[0].Assets[0].BrowserDownloadUrl;

                    // Iniciar o processo de download e extração
                    await DownloadAndExtractZip(downloadUrl);

                }
                else
                {
                    // Não há atualização disponível, fechar o formulário de atualização
                    await Task.Delay(2000);
                    //startProgram();
                    //_ = m_f;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                alertError($"Ocorreu um erro ao baixar a nova atualização: {ex.Message}", "Erro na Atualização!");
                await Task.Delay(3000);
                this.Close();
            }
        }

        private async Task DownloadAndExtractZip(string downloadUrl)
        {
            try
            {
                using (var client = new WebClient())
                {
                    // Evento de progresso do download
                    client.DownloadProgressChanged += (sender, e) =>
                    {
                        progressBar1.Visible = true;
                        progressBar1.Value = e.ProgressPercentage;
                    };

                    string zipFilePath = Path.Combine(Path.GetTempPath(), "update.zip"); // Caminho temporário para salvar o arquivo ZIP baixado

                    // Baixar o zip
                    await client.DownloadFileTaskAsync(new Uri(downloadUrl), zipFilePath);

                    // Extrair o zip para a pasta do programa;
                    ZipFile.ExtractToDirectory(zipFilePath, jsonFolderPath, true);
                    // Gravar version nova.
                    WriteJson("version", latestGitHubVersion.ToString());
                    await Task.Delay(3000);
                    lb_txt.Text = $"Atualização Finalizada!";

                    //startProgram();
                    await Task.Delay(2000);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                alertError($"Ocorreu um erro ao baixar a nova atualização: {ex.Message}", "Erro na Atualização!");
                await Task.Delay(3000);
                this.Close();
            }
        }
        public static void alertError(string txt, string title)
        {
            MessageBox.Show(txt, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void startProgram()
        {
            string otherExecutablePath = Path.Combine(jsonFolderPath, "RcPmGSoft.exe");
            if (File.Exists(otherExecutablePath))
            {
                Process.Start(otherExecutablePath);
            }
            else
            {
                alertError("O executável para abrir não foi encontrado na pasta de instalação.", "Erro ao Inicializar");
            }
        }
    }
}