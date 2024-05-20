using System;
using System.Drawing;
using System.Windows.Forms;

using ButtonClickFunction = System.Action<object, System.EventArgs>;

namespace PowerPoint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _comboBox1.DataSource = Enum.GetValues(typeof(ShapeType));
            
            this.Resize += FormResize;
            _viewModel = new ViewModel();
            _viewModel.ModelChanged += () => Invalidate(true);
            _dataGridView1.DataSource = _viewModel.Shapes;
            
            _toolStripButton1.Click += new EventHandler(ClickToolStripButton(ShapeType.Line));
            _toolStripButton2.Click += new EventHandler(ClickToolStripButton(ShapeType.Rectangle));
            _toolStripButton3.Click += new EventHandler(ClickToolStripButton(ShapeType.Ellipse));
            _toolStripButton4.Click += new EventHandler(GetClickPointer());
            _canvas.MouseDown += _viewModel.HandleCanvasPressed;
            _canvas.MouseUp += _viewModel.HandleCanvasReleased;
            _canvas.MouseMove += _viewModel.HandleCanvasMoved;
            _canvas.Paint += HandleCanvasPaint;
            KeyPreview = true;
            KeyDown += DeleteKeyDown;
            Controls.Add(_canvas);

            _bitmap = new Bitmap(_canvas.Width, _canvas.Height);
            AdjustPanelSize();
        }

        /// <summary>
        /// update
        /// </summary>
        private void UpdatePreview()
        {
            _canvas.DrawToBitmap(_bitmap, new System.Drawing.Rectangle(0, 0, _canvas.Width, _canvas.Height));
            _preview.Image = new Bitmap(_bitmap, _preview.Size);
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                _viewModel.DeleteSelected();
            }
        }

        /// <summary>
        /// update
        /// </summary>
        public void UpdateToolBar()
        {
            _toolStripButton1.Checked = _viewModel.GetCurrentMode() == Mode.Draw && _viewModel.SelectedShape == ShapeType.Line;
            _toolStripButton2.Checked = _viewModel.GetCurrentMode() == Mode.Draw && _viewModel.SelectedShape == ShapeType.Rectangle;
            _toolStripButton3.Checked = _viewModel.GetCurrentMode() == Mode.Draw && _viewModel.SelectedShape == ShapeType.Ellipse;
            _toolStripButton4.Checked = _viewModel.GetCurrentMode() == Mode.Select;
            _toolStripButton5.Enabled = false;
            _toolStripButton6.Enabled = false;
        }

        /// <summary>
        /// click
        /// </summary>
        /// <param name="shapeType"></param>
        /// <returns></returns>
        private ButtonClickFunction ClickToolStripButton(ShapeType shapeType)
        {
            return (sender, e) =>
            {
                _viewModel.SetCurrentMode(Mode.Draw);
                _viewModel.SelectedShape = shapeType;

                UpdateToolBar();
            };
        }

        /// <summary>
        /// handle canvas paint
        /// </summary>
        private void FormResize(object sender, EventArgs e)
        {
            AdjustPanelSize();
            _bitmap = new Bitmap(_canvas.Width, _canvas.Height);
        }

        /// <summary>
        /// handle canvas paint
        /// </summary>
        private void AdjustPanelSize()
        {
            const double RATIO = 16.0 / 9.0; // 16:9的比例
            const int FIFTY = 50;
            UpdatePreview();
            int panelWidth = this.ClientSize.Width - ( _groupBox1.Width + _groupBox2.Width);
            int panelHeight = (int)(panelWidth / RATIO);
            _canvas.Size = new Size(panelWidth, panelHeight);
            _groupBox1.Location = new Point(this.ClientSize.Width - _groupBox1.Width,FIFTY);
            _groupBox1.Height = this.Height;
            _groupBox2.Height = this.Height;
        }

        /// <summary>
        /// click
        /// </summary>
        /// <returns></returns>
        private ButtonClickFunction GetClickPointer()
        {
            return (sender, e) =>
            {
                _viewModel.SetCurrentMode(Mode.Select);

                UpdateToolBar();
            };
        }

        /// <summary>
        /// handle canvas paint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleCanvasPaint(object sender, PaintEventArgs e)
        {
            _viewModel.Draw(e.Graphics);

            UpdateToolBar();
            UpdatePreview();
        }

        /// <summary>
        /// click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1Click(object sender, EventArgs e)
        { 
            var random = new Random();
            const int LOWER_BOUND = 50;
            const int UPPER_BOUND = 400;

            var point1 = new Position();
            var point2 = new Position();
            point1.X = random.Next(LOWER_BOUND, UPPER_BOUND);
            point1.Y = random.Next(LOWER_BOUND, UPPER_BOUND);
            point2.X = random.Next(LOWER_BOUND, UPPER_BOUND);
            point2.Y = random.Next(LOWER_BOUND, UPPER_BOUND);

            _viewModel.Add(ShapeFactory.CreateShape((ShapeType)_comboBox1.SelectedItem, point1, point2));
        }

        /// <summary>
        /// click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickDataGridView1CellContent(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                _viewModel.RemoveAt(e.RowIndex);
            }
        }

        private readonly ViewModel _viewModel;

        private Bitmap _bitmap;
    }
}
