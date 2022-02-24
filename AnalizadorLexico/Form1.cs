using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalizadorLexico
{
    public partial class Form1 : Form
    {

        //   Token1.     Palabra reservada   int, float, double, ....
        //   Token2.     Identificador (variable)
        //   Token3.     Numero entero
        //   Token4.     Numero con punto decimal 
        //   Token5.     Operador Aritmetico   +  -  *   /  ^   
        //   Token6.     Operador Relacional   >  >=  <   <=   !=   ==
        //   Token7.     Operador Logico   &&    ||   ~  
        //   Token8.     Asignacion    =
        //   Token9.     Fin de instruccion     ;             
        //   Token10.    Separador    ,                        
        //   Token11.    Parentesis   (   )                    
        //   Token12.    Corchetes    [   ]                    
        //   Token13.    Llaves       {   }                    
        //   Token14.    Cadena       "Hola"                   
        //   Token15.    Caracter     'z'                      
        //   Token16.    dos puntos  :                        
        //   Token17.    incremento   ++                      
        //   Token18.    Decremento   --                       
        //   Token19.    Operador aritmetivo inclusivo    +=  -=  *=  /=    
        //   Token20.    Punto   .

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtEntrada.Clear();
            txtSalida.Clear();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            string[] PalRes = { "int", "float","print" ,"break", "double", "char", "string", "for", "if", "while", "else", "do", "then"};
            string Buffer = txtEntrada.Text;
            int maximo = Buffer.Length , n=0;
            char ch;
            string lexema, salida = string.Empty;


            for (int ap = 0; ap < maximo; ap++)
            {
                ch = Buffer[ap];
                lexema = string.Empty;
                if (char.IsWhiteSpace(ch)) { }
                else if (char.IsLetter(ch))    // Token 1 y 2 Es una palabra reservada o un identificador
                {
                    while (char.IsLetterOrDigit(ch) || ch == '_')
                    {
                        lexema += ch;
                        ap++; if (ap >= maximo) break;
                        ch = ch = Buffer[ap];
                    }
                    ap--;
                    bool encontrado = false; ;
                    for (int i = 0; i < PalRes.Length; i++) if (PalRes[i] == lexema) encontrado = true;
                    if (encontrado) salida += "Token1 Palabra reservada --> " + lexema + "\r\n";
                    else salida += "Token2 Identificador --> " + lexema + "\r\n";
                }

                else if (char.IsNumber(ch))  //Token 3 y 4  Es un numero entero o con punto decimal
                {
                    while (char.IsNumber(ch) || ch == '.')
                    {
                        lexema += ch;
                        ap++;
                        if (ap >= maximo) break;
                        ch = Buffer[ap];
                        if (ch == '.') n = 1;
                    }
                    if (n == 1)
                    {
                        salida += "Token4  Numero con Punto Decimal --> " + lexema + "\r\n";
                    }
                    else
                    {
                        salida += "Token3  Numero Entero --> " + lexema + "\r\n";
                        ap--;
                    }
                }

                else if (ch == '+') // Token5 Operador Aritmetico
                {
                    lexema += ch;
                    ap++;
                    if (ap >= maximo) break;
                    ch = Buffer[ap];

                    if (ch == '=') // Token 19 Operador Aritmetico Inclusivo
                    {
                        lexema += ch;
                        salida += "Token19 Operador Aritmetico Inclusivo -->" + lexema + "\r\n";
                    }
                    else if (ch == '+') // Token17 Incremento
                    {
                        lexema += ch;
                        salida += "Token17 Incremento -->" + lexema + "\r\n";
                    }
                    else
                    {
                        lexema += ch;
                        salida += "Token5 Operador Aritmetico -->" + lexema + "\r\n";
                    }
                }

                else if (ch == '-')
                {
                    lexema += ch;
                    ap++;
                    if (ap >= maximo) break;
                    ch = Buffer[ap];

                    if (ch == '=') // Token19 Operador Aritmetico Inclusivo
                    {
                        lexema += ch;
                        salida += "Token19 Operador Aritmetico Inclusivo -->" + lexema + "\r\n";
                    }
                    else if (ch == '-')  // Token18 Decremento
                    {
                        lexema += ch;
                        salida += "Token18 Decremento -->" + lexema + "\r\n";
                    }
                    else
                    {
                        lexema += ch;
                        salida += "Token5 Operador Aritmetico -->" + lexema + "\r\n";
                    }
                }

                else if ( ch == '*' || ch == '/')   // Token 5  Operador Aritmetico
                {
                    lexema += ch;
                    ap++;
                    if (ap >= maximo) break;
                    ch = Buffer[ap];

                    if (ch == '=') // Token 19 Operador Aritmetico Inclusivo
                    {
                        lexema += ch;
                        salida += "Token19 Operador Aritmetico Inclusivo -->" + lexema + "\r\n";
                    }
                    else
                    {
                        lexema += ch;
                        salida += "Token5 Operador Aritmetico -->" + lexema + "\r\n";
                    }
                }

                else if (ch == '^') // Token5 Operador Aritmetico potencia
                {
                    lexema += ch;
                    salida += "Token5 Operador Aritmetico -->" + lexema + "\r\n";
                }

                else if (ch == '>' || ch == '<' || ch == '!' || ch == '=')   //  Token 6 y 8
                {
                    lexema += ch;
                    ap++; if (ap >= maximo) break;
                    ch = Buffer[ap];
                    if (ch == '=') salida += "Token6 Operador Relacional --> " + lexema + "=" + "\r\n";
                    else
                    {
                        ap--;
                        if (lexema == ">" || lexema == "<") salida += "Token6 Operador Relacional --> " + lexema + "\r\n";
                        else if (lexema == "=") salida += "Token8 Asignacion --> " + lexema + "\r\n";  // Token 8
                        else salida += "Error --> " + lexema + "\r\n";
                    }
                }


                else if (ch == '|')   // Token7 Operador logico  OR
                {
                    lexema += ch;
                    ap++; if (ap >= maximo) break;
                    ch = Buffer[ap];

                    if (ch == '|') // Token7 Operador Logico
                    {
                        lexema += ch;
                        salida += "Token7 Operador Logico -->" + lexema + "\r\n";
                    }
                    else
                    {
                        lexema += ch;
                        salida += "ERROR DE SINTAXIS ------>" + lexema + "\r\n";
                    }
                }

                else if (ch == '&') // Token7 Operador Logico AND
                {
                    lexema += ch;
                    ap++; if (ap >= maximo) break;
                    ch = Buffer[ap];

                    if (ch == '&') // Token 7 Operador Logico
                    {
                        lexema += ch;
                        salida += "Token7 Operador Logico -->" + lexema + "\r\n";
                    }
                    else
                    {
                        lexema += ch;
                        salida += "ERROR DE SINTAXIS ------>" + lexema + "\r\n";
                    }
                }

                else if (ch == '~')  // Token7   Operador Logico NOT
                {
                    lexema += ch;
                    salida += "Token7  Operador Logico --> " + lexema + "\r\n";
                }

                else if (ch == ';')  // Token 9   Fin de la Instruccion
                {
                    lexema += ch;
                    salida += "Token9  Fin de la Instruccion --> " + lexema + "\r\n";
                }

                else if (ch == ',') // Token 10 Separador
                {
                    lexema += ch;
                    salida += "Token10  Separador --> " + lexema + "\r\n";
                }


                else if (ch == '(' || ch == ')')  // Token 11 Parentesis
                {
                    lexema += ch;
                    salida += "Token11  Parentesis --> " + lexema + "\r\n";
                }

                else if (ch == '[' || ch == ']')  // Token 12 Corchetes
                {
                    lexema += ch;
                    salida += "Token12  Corchete --> " + lexema + "\r\n";
                }

                else if (ch == '{' || ch == '}')  // Token 13 Llaves
                {
                    lexema += ch;
                    salida += "Token13  Llaves --> " + lexema + "\r\n";
                }
               
                else if (ch == '"') // Token 14  Cadena
                {
                    lexema += ch;
                    ap++;
                    if (ap >= maximo) break;
                    ch = Buffer[ap];

                    while (char.IsLetterOrDigit(ch) || char.IsWhiteSpace(ch))
                    {
                        lexema += ch;
                        ap++;
                        if (ap >= maximo) break;
                        ch = Buffer[ap];
                        if (ch == '"')
                        {
                            lexema += ch;
                            break;
                        }
                    }
                    salida += "Token14 Cadena -->" + lexema + "\r\n";
                }

                else if (ch == '\'')//Token15 Carácter
                {
                    lexema += ch;
                    ap++;
                    if (ap >= maximo) break;
                    ch = Buffer[ap];
                    if (char.IsLetterOrDigit(ch))
                        lexema += ch;
                    ap++;
                    if (ap >= maximo) break;
                    ch = Buffer[ap];
                    salida += "Token 15 Caracter --> " + lexema + ch + "\r\n";
                }

                else if (ch == ':')  // Token 16 Dos Puntos
                {
                    lexema += ch;
                    salida += "Token16  Dos Puntos --> " + lexema + "\r\n";
                }

                else if (ch == '.')    //  Token 20
                {
                    salida += "Token20 Punto --> ." + "\r\n";
                }
            }
            txtSalida.Text = salida;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}