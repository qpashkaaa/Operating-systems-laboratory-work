using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Pomoreva.Classes
{
    public class DataGridViewProgressColumn : DataGridViewImageColumn
    {
        public static Color _ProgressBarColor;

        public DataGridViewProgressColumn(double maxValue)
        {
            CellTemplate = new DataGridViewProgressCell();
            this.ProgressBarCellTemplate.SetMaxValue(maxValue);
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(DataGridViewProgressCell)))
                {
                    throw new InvalidCastException("Ячейка должна иметь тип DataGridViewProgressCell");
                }
                base.CellTemplate = value;
            }
        }


        [Browsable(true)]
        public Color ProgressBarColor
        {
            get
            {

                if (this.ProgressBarCellTemplate == null)
                {
                    throw new InvalidOperationException("Операция не может быть завершена, т.к. для данного столбца не задан CellTemplate.");
                }
                return this.ProgressBarCellTemplate.ProgressBarColor;

            }
            set
            {

                if (this.ProgressBarCellTemplate == null)
                {
                    throw new InvalidOperationException("Операция не может быть завершена, т.к. для данного столбца не задан CellTemplate.");
                }
                this.ProgressBarCellTemplate.ProgressBarColor = value;
                if (this.DataGridView != null)
                {
                    DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        DataGridViewProgressCell dataGridViewCell = dataGridViewRow.Cells[this.Index] as DataGridViewProgressCell;
                        if (dataGridViewCell != null)
                        {
                            dataGridViewCell.SetProgressBarColor(rowIndex, value);
                        }
                    }
                    this.DataGridView.InvalidateColumn(this.Index);
                }
            }
        }


        private DataGridViewProgressCell ProgressBarCellTemplate
        {
            get
            {
                return (DataGridViewProgressCell)this.CellTemplate;
            }
        }
    }
}
