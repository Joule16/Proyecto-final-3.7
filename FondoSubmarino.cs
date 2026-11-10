//Suñiga Maciel Joule Alexander
//Villa Olivares Ariel 
//Martinez Marco Antonio
//Equippo 2

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace SistemaBiblioteca1
{
    public class FondoSubmarino
    {
        private class Burbuja
        {
            public float BaseX, Y, Radio, Velocidad, Fase, Amplitud;
        }

        private const int SuperficieY = 22;

        private readonly Form _form;
        private readonly Timer _timer;
        private readonly List<Burbuja> _burbujas = new List<Burbuja>();
        private readonly Random _rnd = new Random();
        private readonly List<Control> _dependientes = new List<Control>();

        private readonly SolidBrush _brochaRayo = new SolidBrush(Color.FromArgb(22, 255, 255, 255));
        private readonly SolidBrush _brochaBurbuja = new SolidBrush(Color.FromArgb(40, 255, 255, 255));
        private readonly SolidBrush _brochaAire = new SolidBrush(Color.FromArgb(80, 225, 245, 255));
        private readonly Pen _bordeBurbuja = new Pen(Color.FromArgb(160, 255, 255, 255), 1.5f);
        private readonly Pen _lineaSuperficie = new Pen(Color.FromArgb(230, 255, 255, 255), 2f);
        private readonly Pen _lineaSuperficie2 = new Pen(Color.FromArgb(90, 255, 255, 255), 1.5f);

        private Bitmap _degradado;
        private Bitmap _frame;
        private float _tiempo;

        public FondoSubmarino(Form form, int cantidadBurbujas = 35)
        {
            _form = form;

            typeof(Control)
                .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(_form, true, null);

            _lineaSuperficie.LineJoin = LineJoin.Round;
            _lineaSuperficie2.LineJoin = LineJoin.Round;

            for (int i = 0; i < cantidadBurbujas; i++)
            {
                _burbujas.Add(CrearBurbuja(true));
            }

            _timer = new Timer { Interval = 40 };
            _timer.Tick += Timer_Tick;

            _form.Paint += Form_Paint;
            _form.Resize += (s, e) => _form.Invalidate();
            _form.FormClosed += Form_FormClosed;

            _timer.Start();
        }

        public void CubrirMdi(MdiClient cliente)
        {
            MethodInfo setStyle = typeof(Control).GetMethod(
                "SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);

            if (setStyle != null)
            {
                setStyle.Invoke(cliente, new object[]
                {
                    ControlStyles.UserPaint |
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw,
                    true
                });
            }

            cliente.Paint += (s, e) => PintarEnControl(e.Graphics, cliente);
            cliente.Invalidate();

            _dependientes.Add(cliente);
        }

        public void Registrar(Control c)
        {
            _dependientes.Add(c);
        }

        public void PintarEnControl(Graphics g, Control c)
        {
            if (_frame == null) return;

            Point p = _form.PointToClient(c.PointToScreen(Point.Empty));
            g.DrawImageUnscaled(_frame, -p.X, -p.Y);
        }

        public void PintarFondo(Graphics g, Rectangle clip)
        {
            Rectangle r = _form.ClientRectangle;
            if (r.Width == 0 || r.Height == 0) return;

            AsegurarBitmaps(r.Size);

            clip = Rectangle.Intersect(clip, r);
            if (clip.Width == 0 || clip.Height == 0) return;

            g.DrawImage(_frame, clip, clip, GraphicsUnit.Pixel);
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            _timer.Stop();
            _timer.Dispose();
            _brochaRayo.Dispose();
            _brochaBurbuja.Dispose();
            _brochaAire.Dispose();
            _bordeBurbuja.Dispose();
            _lineaSuperficie.Dispose();
            _lineaSuperficie2.Dispose();

            if (_degradado != null) _degradado.Dispose();
            if (_frame != null) _frame.Dispose();
        }

        private Burbuja CrearBurbuja(bool posicionAleatoria)
        {
            int ancho = Math.Max(_form.ClientSize.Width, 400);
            int alto = Math.Max(_form.ClientSize.Height, 300);

            return new Burbuja
            {
                BaseX = _rnd.Next(0, ancho),
                Y = posicionAleatoria ? _rnd.Next(SuperficieY, alto) : alto + 20,
                Radio = _rnd.Next(3, 14),
                Velocidad = 0.6f + (float)_rnd.NextDouble() * 1.6f,
                Fase = (float)(_rnd.NextDouble() * Math.PI * 2),
                Amplitud = _rnd.Next(5, 25)
            };
        }

        private void AsegurarBitmaps(Size tam)
        {
            if (_frame != null && _frame.Width == tam.Width && _frame.Height == tam.Height)
                return;

            if (_degradado != null) _degradado.Dispose();
            if (_frame != null) _frame.Dispose();

            _degradado = new Bitmap(tam.Width, tam.Height, PixelFormat.Format32bppPArgb);
            _frame = new Bitmap(tam.Width, tam.Height, PixelFormat.Format32bppPArgb);

            using (Graphics gb = Graphics.FromImage(_degradado))
            {
                using (var pincel = new LinearGradientBrush(
                    new Rectangle(0, 0, tam.Width, tam.Height),
                    Color.FromArgb(0, 170, 255),
                    Color.FromArgb(5, 20, 120),
                    90f))
                {
                    gb.FillRectangle(pincel, 0, 0, tam.Width, tam.Height);
                }

                using (var brillo = new LinearGradientBrush(
                    new Rectangle(0, SuperficieY - 1, tam.Width, 72),
                    Color.FromArgb(110, 255, 255, 255),
                    Color.FromArgb(0, 255, 255, 255),
                    90f))
                {
                    gb.FillRectangle(brillo, 0, SuperficieY - 1, tam.Width, 72);
                }
            }

            DibujarCuadro();
        }

        private float AlturaOla(float x, float desfase)
        {
            return SuperficieY + desfase
                   + (float)Math.Sin(x * 0.020f + _tiempo * 2.0f) * 3f
                   + (float)Math.Sin(x * 0.055f - _tiempo * 1.4f) * 1.5f;
        }

        private void DibujarCuadro()
        {
            int w = _frame.Width;
            int h = _frame.Height;

            using (Graphics g = Graphics.FromImage(_frame))
            {
                g.DrawImageUnscaled(_degradado, 0, 0);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                for (int i = 0; i < 5; i++)
                {
                    float x = w * (0.1f + i * 0.2f)
                              + (float)Math.Sin(_tiempo * 0.5f + i) * 40f;

                    PointF[] rayo =
                    {
                        new PointF(x, 0),
                        new PointF(x + 80, 0),
                        new PointF(x + 200, h * 0.8f),
                        new PointF(x + 60, h * 0.8f)
                    };
                    g.FillPolygon(_brochaRayo, rayo);
                }

                int paso = 8;
                int n = w / paso + 2;
                PointF[] ola = new PointF[n];
                PointF[] ola2 = new PointF[n];
                PointF[] aire = new PointF[n + 2];

                for (int i = 0; i < n; i++)
                {
                    float x = i * paso;
                    ola[i] = new PointF(x, AlturaOla(x, 0f));
                    ola2[i] = new PointF(x, AlturaOla(x + 40f, 7f));
                    aire[i] = ola[i];
                }

                aire[n] = new PointF((n - 1) * paso, 0);
                aire[n + 1] = new PointF(0, 0);

                g.FillPolygon(_brochaAire, aire);
                g.DrawLines(_lineaSuperficie2, ola2);
                g.DrawLines(_lineaSuperficie, ola);

                foreach (Burbuja b in _burbujas)
                {
                    float x = b.BaseX + (float)Math.Sin(b.Fase + _tiempo * 2f) * b.Amplitud;
                    g.FillEllipse(_brochaBurbuja, x, b.Y, b.Radio * 2, b.Radio * 2);
                    g.DrawEllipse(_bordeBurbuja, x, b.Y, b.Radio * 2, b.Radio * 2);
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_form.WindowState == FormWindowState.Minimized) return;

            Size tam = _form.ClientSize;
            if (tam.Width == 0 || tam.Height == 0) return;

            AsegurarBitmaps(tam);

            _tiempo += 0.04f;

            for (int i = 0; i < _burbujas.Count; i++)
            {
                _burbujas[i].Y -= _burbujas[i].Velocidad;

                if (_burbujas[i].Y < SuperficieY)
                {
                    _burbujas[i] = CrearBurbuja(false);
                }
            }

            DibujarCuadro();
            _form.Invalidate();

            foreach (Control c in _dependientes)
            {
                c.Invalidate();
            }
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = _form.ClientRectangle;
            if (r.Width == 0 || r.Height == 0) return;

            AsegurarBitmaps(r.Size);

            Rectangle clip = e.ClipRectangle;
            if (clip.Width == 0 || clip.Height == 0) clip = r;
            clip = Rectangle.Intersect(clip, r);
            if (clip.Width == 0 || clip.Height == 0) return;

            e.Graphics.DrawImage(_frame, clip, clip, GraphicsUnit.Pixel);
        }
    }
}