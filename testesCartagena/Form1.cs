using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CartagenaServer;

namespace testesCartagena
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //escopo global do form1

        int idPartida;

        int idJogador;
        string senhaJogador;
        string corJogador;

        string[] cartasSeparadas;
        string cartaParaJogar;
        int numCartaAjogar;

        string[] casasSeparadas;
        string tipoCasaSel;
        int numCasaSel;

        //////////////

        private void listar_btn_Click(object sender, EventArgs e)
        {
            //separa as partidas removendo o \r
            string listaPartidas;
            listaPartidas = Jogo.ListarPartidas("T");
            listaPartidas = listaPartidas.Replace("\r", "");

            //separa em string \n
            /*
             idPartida,nomePartida,DataPartida\n
             */
            string[] partidas = listaPartidas.Split('\n');
            //adiciona numa listbox
            for (int i = 0; i < partidas.Length; i++)
            {
                partidas_lbx.Items.Add(partidas[i]);
            }
        }

        private void selecionar_btn_Click(object sender, EventArgs e)
        {
            //pega item selecionado na list box de partidas
            string partida = partidas_lbx.SelectedItem.ToString();
            string[] itens = partida.Split(',');

            //separa os itens ',' ficado id nome e data 
            idPartida = Convert.ToInt32(itens[0]);
            string nomePartida = itens[1];
            string dataPartida = itens[2];
            string statusPartidas = itens[3];

            //mostra partida selecionada
            partidaSel_lbl.Text = "id: " + idPartida.ToString() + "\nNome Partida: " + nomePartida;

        }

        private void criarPartida_btn_Click(object sender, EventArgs e)
        {
            //recebe nome e senha da partida a ser criada
            string nomeNovo = nomePartida_tbx.Text;
            string senhaNova = senha_tbx.Text;

            //cria a partida por si so
            string retorno = Jogo.CriarPartida(nomeNovo, senhaNova);
        }

        private void entrarPartida_btn_Click(object sender, EventArgs e)
        {
            //recebe nome do jogador(Novo) e senha da partida para  entrar
            string nome = nomeJogador_tbx.Text;
            string senha = senhaEntrar_tbx.Text;

            //retorna senha do jogador
            string retorno = Jogo.EntrarPartida(idPartida, nome, senha);
            //testa se ja exite mesmo jogador
            if (retorno.StartsWith("ERRO"))
            {
                MessageBox.Show(retorno, "PI Cartagena", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            retorno = retorno.Replace("\r", "");
            //imprime todo retorno do novo jogador
            senhaJogador_lbl.Text = retorno;

            //separa retorno do jogador
            string[] itensJogador = retorno.Split(',');
            idJogador = Convert.ToInt32(itensJogador[0]);
            senhaJogador = itensJogador[1];
            corJogador = itensJogador[2];



        }


        //inicia partida
        private void jogar_btn_Click(object sender, EventArgs e)
        {
            //exibe o jogador a comecar
            jogando_lbl.Text = Jogo.IniciarPartida(idJogador, senhaJogador);
            

        }

        private void exibirBoard_btn_Click(object sender, EventArgs e)
        {
            //tebuleiro por completo (Casa,Tipo)
            string tab = Jogo.ExibirTabuleiro(idPartida);
            //imprime tabuleiro na list box (tabuleiro_lbx)
            tab.Replace("\r", "");
            casasSeparadas = tab.Split('\n');
            for (int i = 0; i < casasSeparadas.Length; i++)
            {
                tabuleiro_lbx.Items.Add(casasSeparadas[i]);
            }


        }


        private void consultarMao_btn_Click(object sender, EventArgs e)
        {
            //recebe as cartas na mao do jogador (Tipo,num de Cartas)
            string cartas = Jogo.ConsultarMao(idJogador, senhaJogador);

            //separa as caratas no array cartasSeparadas;
            cartas.Replace("\r", "");
            cartasSeparadas = cartas.Split('\n');

            //imprime mao numa listbox
            for (int i = 0; i < cartasSeparadas.Length; i++)
            {
                mao_lbx.Items.Add(cartasSeparadas[i]);
            }


        }

        private void cartaSel_btn_Click(object sender, EventArgs e)
        {

            //pega a informacao da cara selecionada na listbox da mao
            string cartaSel = mao_lbx.SelectedItem.ToString();

            //separa carta selecionada (Tipo,numCartas)
            string[] cartaSelecionada = cartaSel.Split(',');

            cartaParaJogar = Convert.ToString(cartaSelecionada[0]);

            numCartaAjogar = Convert.ToInt32(cartaSelecionada[1]);

            cartaSel_lbl.Text = cartaParaJogar + " " + Convert.ToString(numCartaAjogar);
        }

        private void casaSel_btn_Click(object sender, EventArgs e)
        {
            //pega informacao da casa selecionada

            string casas = tabuleiro_lbx.SelectedItem.ToString();
            string[] casasSele = casas.Split(',');
            //corta info em num e tipo casa

            //convert informacoes e as passa para as variaveis a ser tratada
            numCasaSel = Convert.ToInt32(casasSele[0]);
            tipoCasaSel = Convert.ToString(casasSele[1]);

            casaSel_lbl.Text = tipoCasaSel + " " + Convert.ToString(numCasaSel);
        }
        private void andar_btn_Click(object sender, EventArgs e)
        {
            /*
            if(cartaParaJogar!= tipoCasaSel)
            {
                MessageBox.Show(Convert.ToString(tipoCasaSel), "PI Cartagena", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
            jogada.Text = Jogo.Jogar(idJogador, senhaJogador, numCasaSel, cartaParaJogar);

        }

        private void vv_btn_Click(object sender, EventArgs e)
        {
            string retorno;
            retorno = Jogo.VerificarVez(idPartida);
            retorno = retorno.Replace("\r", "");

            string[] lista = retorno.Split('\n');
            for (int i = 0; i < lista.Length; i++)
            {
                listaAtt_lbx.Items.Add(lista[i]);
            }


        }
    }
}

