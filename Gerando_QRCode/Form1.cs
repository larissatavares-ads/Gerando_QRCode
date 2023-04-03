using System.Drawing.Drawing2D;
using System.Drawing.Text;
using ZXing.Windows.Compatibility;

namespace Gerando_QRCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtTexto_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtTexto.Text == string.Empty || txtLargura.Text == string.Empty && txtLargura.Text == string.Empty)
            {
                MessageBox.Show("Informações inválidas. Complete as informações para gerar o QRCode...");
                txtTexto.Focus();
                return;
            }
            try
            {
                int largura = Convert.ToInt32(txtLargura.Text);
                int altura = Convert.ToInt32(txtAltura.Text);
                picQRCode.Image = GerarQRCode(largura, altura, txtTexto.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Bitmap GerarQRCode(int width, int height, string text)
        {
            try
            {
                var bw = new BarcodeWriter();
                var encOptions = new ZXing.Common.EncodingOptions() { Width = width, Height = height, Margin = 0 };
                //bw.Renderer = new BitmapRenderer();
                bw.Options = encOptions;
                bw.Format = ZXing.BarcodeFormat.QR_CODE;
                var code = new Bitmap(bw.Write(text));
                var logo = new Bitmap($"{Application.StartupPath}/piclerick.png");
                
                RectangleF rectf = new RectangleF(30, 10, (code.Width - logo.Width) / 2, (code.Height - logo.Height) / 2);
                
                Graphics g = Graphics.FromImage(code);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                StringFormat format = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                g.DrawRectangle(Pens.Red, Rectangle.Round(rectf));
                g.DrawString("1", new Font("Tahoma", 50), Brushes.Red, rectf, format);
                g.Flush();
                //g.DrawImage(logo, new Point((code.Width - logo.Width)/2, (code.Height - logo.Height)/2));
                
                return code;
            }
            catch
            {
                throw;
            }
        }

        private void picQRCode_Click(object sender, EventArgs e)
        {

        }
    }
}