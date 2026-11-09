// Actividad 3.7 - Equipo #2
//   NUÑEZ Martinez Marco Antonio
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
// ===============================
using System;
using System.Windows.Forms;

namespace SistemaBiblioteca1
{
    public static class AyudaTablas
    {
        public static void Ajustar(DataGridView tabla)
        {
            if (tabla.Columns.Count == 0)
                return;

            tabla.ScrollBars = ScrollBars.Both;
            tabla.AllowUserToResizeColumns = true;
            tabla.ShowCellToolTips = true;

            tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            int[] anchos = new int[tabla.Columns.Count];
            for (int i = 0; i < tabla.Columns.Count; i++)
                anchos[i] = tabla.Columns[i].Width;
            for (int i = 0; i < tabla.Columns.Count; i++)
            {
                DataGridViewColumn columna = tabla.Columns[i];
                columna.MinimumWidth = Math.Max(20, anchos[i] + 8);
            }

            tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            for (int i = 0; i < tabla.Columns.Count; i++)
            {
                DataGridViewColumn columna = tabla.Columns[i];
                columna.FillWeight = Math.Max(1, anchos[i]);
                columna.ToolTipText = columna.HeaderText;
            }

            foreach (DataGridViewRow fila in tabla.Rows)
            {
                foreach (DataGridViewCell celda in fila.Cells)
                    celda.ToolTipText = Convert.ToString(celda.FormattedValue) ?? string.Empty;
            }
        }
    }
}