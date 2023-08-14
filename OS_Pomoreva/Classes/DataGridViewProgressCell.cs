using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Pomoreva.Classes
{
    class DataGridViewProgressCell : DataGridViewImageCell
    {
        // Используется для соответствия типу DataGridViewImageCell
        static Image emptyImage;
        // Используется для хранения цвета заливки прогресс-бара
        static Color _ProgressBarColor;

        private double maxValue = 0;

        public Color ProgressBarColor
        {
            get { return _ProgressBarColor; }
            set { _ProgressBarColor = value; }
        }

        static DataGridViewProgressCell()
        {
            emptyImage = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }
        public DataGridViewProgressCell()
        {
            this.ValueType = typeof(int);
        }
        // Метод требуется для соответствия Progress Cell типу ячейки по умолчанию Image Cell.
        // Ячейка по умолчанию Image Cell обрабатывает изображение как значение, а в нашей ячейке это значением является целое число.
        protected override object GetFormattedValue(object value,
            int rowIndex, ref DataGridViewCellStyle cellStyle,
            TypeConverter valueTypeConverter,
            TypeConverter formattedValueTypeConverter,
            DataGridViewDataErrorContexts context)
        {
            return emptyImage;
        }

        protected override void Paint(System.Drawing.Graphics g,
            System.Drawing.Rectangle clipBounds,
            System.Drawing.Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates cellState,
            object value, object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            if (Convert.ToInt16(value) == 0 || value == null)
            {
                value = 0;
            }

            int progressVal = Convert.ToInt32(value);

            int percentage = progressVal;
            Brush backColorBrush = new SolidBrush(Color.Black);
            Brush foreColorBrush = new SolidBrush(Color.Black);

            // Рисование рамки ячейки
            base.Paint(g, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));

            float posX = cellBounds.X;
            float posY = cellBounds.Y;

            float textWidth = TextRenderer.MeasureText(progressVal.ToString(), cellStyle.Font).Width;
            float textHeight = TextRenderer.MeasureText(progressVal.ToString(), cellStyle.Font).Height;

            // Положение текста в зависимости от выравнивания в ячейке
            switch (cellStyle.Alignment)
            {
                case DataGridViewContentAlignment.BottomCenter:
                    posX = cellBounds.X + (cellBounds.Width / 2) - textWidth / 2;
                    posY = cellBounds.Y + cellBounds.Height - textHeight;
                    break;
                case DataGridViewContentAlignment.BottomLeft:
                    posX = cellBounds.X;
                    posY = cellBounds.Y + cellBounds.Height - textHeight;
                    break;
                case DataGridViewContentAlignment.BottomRight:
                    posX = cellBounds.X + cellBounds.Width - textWidth;
                    posY = cellBounds.Y + cellBounds.Height - textHeight;
                    break;
                case DataGridViewContentAlignment.MiddleCenter:
                    posX = cellBounds.X + (cellBounds.Width / 2) - textWidth / 2;
                    posY = cellBounds.Y + (cellBounds.Height / 2) - textHeight / 2;
                    break;
                case DataGridViewContentAlignment.MiddleLeft:
                    posX = cellBounds.X;
                    posY = cellBounds.Y + (cellBounds.Height / 2) - textHeight / 2;
                    break;
                case DataGridViewContentAlignment.MiddleRight:
                    posX = cellBounds.X + cellBounds.Width - textWidth;
                    posY = cellBounds.Y + (cellBounds.Height / 2) - textHeight / 2;
                    break;
                case DataGridViewContentAlignment.TopCenter:
                    posX = cellBounds.X + (cellBounds.Width / 2) - textWidth / 2;
                    posY = cellBounds.Y;
                    break;
                case DataGridViewContentAlignment.TopLeft:
                    posX = cellBounds.X;
                    posY = cellBounds.Y;
                    break;

                case DataGridViewContentAlignment.TopRight:
                    posX = cellBounds.X + cellBounds.Width - textWidth;
                    posY = cellBounds.Y;
                    break;

            }

            double WidthElemCell = (cellBounds.Width - 5) / (double)cellBounds.Width; //это добавил
            if (percentage < 0)
            {
                // При отрицательном значении отображается только текст
                if (this.DataGridView.CurrentRow.Index == rowIndex)
                {
                    g.DrawString(progressVal.ToString(), cellStyle.Font, new SolidBrush(cellStyle.SelectionForeColor), posX, posX);
                }
                else
                {
                    g.DrawString(progressVal.ToString(), cellStyle.Font, foreColorBrush, posX, posY);
                }
            }

            if (percentage == 0)
            {
                Size SizeString = TextRenderer.MeasureText("Не запускалось", cellStyle.Font);
                if (cellBounds.Width > SizeString.Width)
                {
                    g.DrawString("Не запускалось", cellStyle.Font, new SolidBrush(Color.Black), cellBounds.X + (cellBounds.Width - SizeString.Width) / 2, posY); //в этой строке и в похожих строках ниже, заменил
                }
                else
                {
                    // Рисование прогресса
                    for (int i = 0; i < cellBounds.Height - 5; i++)
                    {
                        g.FillRectangle(new SolidBrush(_ProgressBarColor), cellBounds.X + 2, cellBounds.Y + 2 + i, Convert.ToInt32(((percentage * (cellBounds.Width-5)) / maxValue )), (cellBounds.Height - 10) / 10);
                    }

                    // Рисование текста
                    g.DrawString(progressVal.ToString(), cellStyle.Font, foreColorBrush, posX, posY);
                }
            }

            if (percentage > 0.0)
            {

                // Рисование прогресса
                for (int i = 0; i < cellBounds.Height - 5; i++)
                {
                    g.FillRectangle(new SolidBrush(_ProgressBarColor), cellBounds.X + 2, cellBounds.Y + 2 + i, Convert.ToInt32(((percentage * (cellBounds.Width-5)) / maxValue )), (cellBounds.Height - 10) / 10);
                }

                // Рисование текста
                g.DrawString(progressVal.ToString(), cellStyle.Font, foreColorBrush, posX, posY);
            }

            if (percentage >= 100.0)
            {
                Size SizeString = TextRenderer.MeasureText("Выполнено", cellStyle.Font);
                if (cellBounds.Width > SizeString.Width)
                {
                    for (int i = 0; i < cellBounds.Height - 5; i++)
                    {
                        g.FillRectangle(new SolidBrush(_ProgressBarColor), cellBounds.X + 2, cellBounds.Y + 2 + i, Convert.ToInt32(((percentage * (cellBounds.Width-5)) / maxValue )), (cellBounds.Height - 10) / 10);
                    }
                    g.DrawString("Выполнено", cellStyle.Font, foreColorBrush, cellBounds.X + (cellBounds.Width - SizeString.Width) / 2, posY);
                }
                else
                {
                    // Рисование прогресса
                    for (int i = 0; i < cellBounds.Height - 5; i++)
                    {
                        g.FillRectangle(new SolidBrush(_ProgressBarColor), cellBounds.X + 2, cellBounds.Y + 2 + i, Convert.ToInt32(((percentage * (cellBounds.Width-5)) / maxValue )), (cellBounds.Height - 10) / 10);
                    }

                    // Рисование текста
                    g.DrawString(progressVal.ToString(), cellStyle.Font, foreColorBrush, posX, posY);
                }
            }

        }

        public override object Clone()
        {
            DataGridViewProgressCell dataGridViewCell = base.Clone() as DataGridViewProgressCell;
            if (dataGridViewCell != null)
            {
                dataGridViewCell.ProgressBarColor = this.ProgressBarColor;
            }
            return dataGridViewCell;
        }

        internal void SetProgressBarColor(int rowIndex, Color value)
        {
            this.ProgressBarColor = value;
        }

        public void SetMaxValue(double value)
        {
            this.maxValue = value;
        }

    }
}
