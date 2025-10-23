using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using PlanHora.Views;

namespace PlanHora.Converters
{
    public class HorarioFormatterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is HorarioDia dia)
            {
                if (!string.IsNullOrEmpty(dia.HoraEntrada) || !string.IsNullOrEmpty(dia.HoraSalida))
                    return $"Entrada - {dia.HoraEntrada}\nSalida - {dia.HoraSalida}";
                return string.Empty;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
