using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using System.Globalization;

namespace MauiTAbPage;

public partial class MenuPrincipal : Microsoft.Maui.Controls.TabbedPage
{
    public MenuPrincipal()
    {
        InitializeComponent();
        On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetToolbarPlacement(
               Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom);
    }

    private void OnClassificarClicked(object sender, EventArgs e)
    {
        var idadeEntry = this.FindByName<Microsoft.Maui.Controls.Entry>("entryIdade");
        var rendaEntry = this.FindByName<Microsoft.Maui.Controls.Entry>("entryRenda");
        var resultadoLabel = this.FindByName<Label>("lblResultado");

       
        string idadeTexto = idadeEntry?.Text?.Replace(" anos", "").Trim();
        string rendaTexto = rendaEntry?.Text?.Replace("R$", "").Trim();

        if (int.TryParse(idadeTexto, out int idade) &&
            double.TryParse(rendaTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out double renda))
        {
            string score;
            string cor;

            if (idade > 30 && renda > 5000)
            {
                score = "Score Alto";
                cor = "#43a047";
            }
            else if (idade > 25 && renda > 3000)
            {
                score = "Score Médio";
                cor = "ffc107";
            }
            else if (idade > 18 && renda > 1500)
            {
                score = "Score Baixo";
                cor = "#fb8c00";
            }
            else
            {
                score = "Score Muito Baixo";
                cor = "#e53935";
            }

            resultadoLabel.Text = $"Resultado: {score}";
            resultadoLabel.TextColor = Color.FromArgb(cor);
        }
        else
        {
            resultadoLabel.Text = "Preencha corretamente os campos.";
            resultadoLabel.TextColor = Colors.Red;
        }
    }

    private void OnIniciarClicked(object sender, EventArgs e)
    {
        this.CurrentPage = this.Children.OfType<ContentPage>().FirstOrDefault(page => page.Title == "Score");
    }

    private void OnLimparClicked(object sender, EventArgs e)
    {
        var idadeEntry = this.FindByName<Microsoft.Maui.Controls.Entry>("entryIdade");
        var rendaEntry = this.FindByName<Microsoft.Maui.Controls.Entry>("entryRenda");
        var resultadoLabel = this.FindByName<Label>("lblResultado");

        idadeEntry.Text = string.Empty;
        rendaEntry.Text = string.Empty;
        resultadoLabel.Text = string.Empty;
    }

    private bool _editandoIdade = false;
    private void OnIdadeChanged(object sender, TextChangedEventArgs e)
    {
        if (_editandoIdade) return;
        _editandoIdade = true;

        var entry = (Microsoft.Maui.Controls.Entry)sender;
        string texto = e.NewTextValue?.Replace(" anos", "") ?? "";

        if (int.TryParse(texto, out int idade))
        {
            entry.Text = $"{idade} anos";
        }
        else
        {
            entry.Text = "";
        }

        _editandoIdade = false;
    }

    private bool _editandoRenda = false;
    private void OnRendaChanged(object sender, TextChangedEventArgs e)
    {
        if (_editandoRenda) return;
        _editandoRenda = true;

        var entry = (Microsoft.Maui.Controls.Entry)sender;
        string texto = e.NewTextValue?.Replace("R$", "").Replace(",", "").Trim() ?? "";

        if (double.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out double renda))
        {
            entry.Text = $"R$ {renda:0.00}";
        }
        else
        {
            entry.Text = "";
        }

        _editandoRenda = false;
    }
}
