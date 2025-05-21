using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConversorDeBases
{
    public partial class frm_calculadoraDeBases : Form
    {
        public frm_calculadoraDeBases()
        {
            InitializeComponent();
        }

        // Expressões regulares para validação dos formatos de entrada
        string regexDecimal = @"^[0-9]\d*$";
        string regexBinario = @"^[0-1]*$";
        string regexHexa = @"^[A-Fa-f0-9]*$";

        private void frm_calculadoraDeBases_Load(object sender, EventArgs e)
        {
            cbx_operacao.SelectedIndex = 0; // Define a operação padrão selecionada na ComboBox
        }

        private void btn_calcular_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtém o RadioButton selecionado dentro do painel
                RadioButton rbt_selecionado = panel_rbts.Controls.OfType<RadioButton>().Where(x => x.Checked).FirstOrDefault();

                // Verifica qual base foi selecionada
                switch (rbt_selecionado.Text)
                {
                    case "Decimal":
                        // Valida se os valores informados estão no formato decimal
                        if (!Regex.IsMatch(txt_num1.Text, regexDecimal))
                        {
                            txt_num1.Focus();
                            throw new Exception("Número 1 não está no formato decimal! Digite apenas números positivos");
                        }
                        else if (!Regex.IsMatch(txt_num2.Text, regexDecimal))
                        {
                            txt_num2.Focus();
                            throw new Exception("Número 2 não está no formato decimal! Digite apenas números positivos");
                        }

                        // Converte os valores para inteiro
                        int decimal_num1 = int.Parse(txt_num1.Text);
                        int decimal_num2 = int.Parse(txt_num2.Text);

                        // Executa a operação selecionada
                        switch (cbx_operacao.SelectedItem)
                        {
                            case "+":
                                txt_resultado.Text = (decimal_num1 + decimal_num2).ToString(); // Soma
                                break;

                            case "-":
                                if (decimal_num1 < decimal_num2) // Subtração inválida
                                    MessageBox.Show("O programa não suporta esta operação! O Número 2 é maior que o Número 1", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else
                                    txt_resultado.Text = (decimal_num1 - decimal_num2).ToString(); // Subtração válida
                                break;

                            case "*":
                                txt_resultado.Text = (decimal_num1 * decimal_num2).ToString(); // Multiplicação
                                break;
                        }
                        break;

                    case "Binário":
                        // Valida se os valores informados estão no formato binário
                        if (!Regex.IsMatch(txt_num1.Text, regexBinario))
                        {
                            txt_num1.Focus();
                            throw new Exception("Número 1 não está no formato binário! Digite apenas 0 e 1");
                        }
                        else if (!Regex.IsMatch(txt_num2.Text, regexBinario))
                        {
                            txt_num2.Focus();
                            throw new Exception("Número 2 não está no formato binário! Digite apenas 0 e 1");
                        }

                        // Converte os valores binários para inteiro
                        int binario_num1 = Convert.ToInt32(txt_num1.Text, 2);
                        int binario_num2 = Convert.ToInt32(txt_num2.Text, 2);
                        string resultado, binarioNorm;

                        // Executa a operação selecionada
                        switch (cbx_operacao.SelectedItem)
                        {
                            case "+":
                                resultado = Convert.ToString(binario_num1 + binario_num2, 2); // Soma binária
                                while (resultado.Length % 4 != 0)
                                    resultado = "0" + resultado; // Preenche à esquerda com zeros até completar múltiplos de 4 bits

                                binarioNorm = "";
                                for (int i = 0; i < resultado.Length; i += 4)
                                    binarioNorm += resultado.Substring(i, 4) + " "; // Agrupa em blocos de 4 bits

                                binarioNorm = binarioNorm.TrimEnd(); // Remove espaço no final
                                txt_resultado.Text = binarioNorm; // Exibe o resultado formatado
                                break;

                            case "-":
                                if (binario_num1 < binario_num2) // Subtração inválida
                                    MessageBox.Show("O programa não suporta esta operação! O Número 2 é maior que o Número 1", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else
                                {
                                    resultado = Convert.ToString(binario_num1 - binario_num2, 2); // Subtração binária
                                    while (resultado.Length % 4 != 0)
                                        resultado = "0" + resultado;

                                    binarioNorm = "";
                                    for (int i = 0; i < resultado.Length; i += 4)
                                        binarioNorm += resultado.Substring(i, 4) + " ";

                                    binarioNorm = binarioNorm.TrimEnd();
                                    txt_resultado.Text = binarioNorm;
                                }
                                break;

                            case "*":
                                resultado = Convert.ToString(binario_num1 * binario_num2, 2); // Multiplicação binária
                                while (resultado.Length % 4 != 0)
                                    resultado = "0" + resultado;

                                binarioNorm = "";
                                for (int i = 0; i < resultado.Length; i += 4)
                                    binarioNorm += resultado.Substring(i, 4) + " ";

                                binarioNorm = binarioNorm.TrimEnd();
                                txt_resultado.Text = binarioNorm;
                                break;
                        }
                        break;

                    case "Hexadecimal":
                        // Valida se os valores informados estão no formato hexadecimal
                        if (!Regex.IsMatch(txt_num1.Text, regexHexa))
                        {
                            txt_num1.Focus();
                            throw new Exception("Número 1 não está no formato hexadecimal! Digite apenas as letras A, B, C, D, E e F");
                        }
                        else if (!Regex.IsMatch(txt_num2.Text, regexHexa))
                        {
                            txt_num2.Focus();
                            throw new Exception("Número 2 não está no formato hexadecimal! Digite apenas as letras A, B, C, D, E e F");
                        }

                        // Converte os valores hexadecimais para inteiro
                        int hexa_num1 = Convert.ToInt32(txt_num1.Text.ToLower(), 16);
                        int hexa_num2 = Convert.ToInt32(txt_num2.Text.ToLower(), 16);

                        // Executa a operação selecionada
                        switch (cbx_operacao.SelectedItem)
                        {
                            case "+":
                                txt_resultado.Text = (hexa_num1 + hexa_num2).ToString("X"); // Soma em hexadecimal
                                break;

                            case "-":
                                if (hexa_num1 < hexa_num2) // Subtração inválida
                                    MessageBox.Show("O programa não suporta esta operação! O Número 2 é maior que o Número 1", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else
                                    txt_resultado.Text = (hexa_num1 - hexa_num2).ToString("X"); // Subtração em hexadecimal
                                break;

                            case "*":
                                txt_resultado.Text = (hexa_num1 * hexa_num2).ToString("X"); // Multiplicação em hexadecimal
                                break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                // Exibe mensagem de erro caso ocorra alguma exceção
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_voltar_Click(object sender, EventArgs e)
        {
            this.Close(); // Fecha o formulário atual
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
