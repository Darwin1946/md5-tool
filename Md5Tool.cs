using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;

class Md5Form : Form {
    TextBox path = new TextBox(), result = new TextBox();
    Button browse = new Button(), compute = new Button(), copy = new Button();
    Label status = new Label();
    bool busy;
    public Md5Form() {
        Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath); Text = "MD5 文件校验工具"; ClientSize = new Size(640, 280);
        Font = new Font("Microsoft YaHei UI", 10); StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle; MaximizeBox = false; AllowDrop = true;
        var title = new Label { Text = "将文件拖到窗口，或点击“选择文件”", AutoSize = true, Location = new Point(24,24) };
        path.SetBounds(24,60,475,30); path.ReadOnly = true;
        browse.Text = "选择文件"; browse.SetBounds(510,58,106,34);
        compute.Text = "计算 MD5"; compute.SetBounds(24,110,140,38);
        result.SetBounds(24,171,475,30); result.ReadOnly = true; result.Font = new Font("Consolas",12);
        copy.Text = "复制 MD5"; copy.SetBounds(510,168,106,36); copy.Enabled = false;
        status.SetBounds(24,226,590,30); status.Text = "请选择一个文件。";
        Controls.AddRange(new Control[]{title,path,browse,compute,result,copy,status});
        DragEnter += (s,e) => { if (!busy && e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy; };
        DragDrop += (s,e) => { var files = (string[])e.Data.GetData(DataFormats.FileDrop); if(files.Length != 1) { status.Text = "请一次拖入一个文件。"; return; } SelectFile(files[0]); };
        browse.Click += (s,e) => { using(var dialog = new OpenFileDialog()) { if(dialog.ShowDialog() == DialogResult.OK) SelectFile(dialog.FileName); } };
        compute.Click += async (s,e) => await Compute();
        copy.Click += (s,e) => { try { Clipboard.SetText(result.Text); status.Text = "MD5 已复制到剪贴板。"; } catch(Exception ex) { MessageBox.Show(this, ex.Message, "复制失败"); } };
    }
    void SelectFile(string file) {
        if(busy) return;
        if(!File.Exists(file)) { status.Text = "请选择文件，暂不支持文件夹。"; return; }
        path.Text = file; result.Clear(); copy.Enabled = false; status.Text = "文件已选择，点击“计算 MD5”。";
    }
    async Task Compute() {
        if(busy) return;
        if(!File.Exists(path.Text)) { status.Text = "请先选择有效的文件。"; return; }
        busy = true; browse.Enabled = compute.Enabled = copy.Enabled = false; result.Clear(); status.Text = "正在计算，请稍候…";
        string file = path.Text;
        try {
            string hash = await Task.Run(() => HashFile(file));
            if(IsDisposed) return;
            result.Text = hash; copy.Enabled = true; status.Text = "计算完成。";
        } catch(Exception ex) { if(!IsDisposed) status.Text = "计算失败：" + ex.Message; }
        finally { if(!IsDisposed) { busy = false; browse.Enabled = compute.Enabled = true; } }
    }
    public static string HashFile(string file) {
        using(var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
        using(var md5 = MD5.Create()) return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLowerInvariant();
    }
    [STAThread] static void Main(string[] args) {
        if(args.Length == 2 && args[0] == "--hash") { File.WriteAllText(args[1] + ".md5", HashFile(args[1])); return; }
        Application.EnableVisualStyles(); Application.SetCompatibleTextRenderingDefault(false); Application.Run(new Md5Form());
    }
}

