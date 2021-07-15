<%@ Page Title="Magia das Cartas - Baralho Cigano do Zero ao Avançado" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PagNani._Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        video {
            max-width: 100%;
            height: auto;
        }

        #tema .imgTema24 img {
            display: block;
            margin: auto;
            max-width: 500px;
            width: 100%;
        }
        .tema-topicos {
            display:block;
            margin: auto;
            width: 600px;
            margin-top: 20px;
        }
        #garantir-vaga .imgVaga img {
            margin: 50px 0 30px;
            width: 100%;
        }
    </style>


    <section id="energia-mistica">
        <div class="container">
            <h2 class="text-center">Você também é apaixonada(o) pelo universo cigano e por toda essa energia mística?</h2>

            <h3 style="font-weight: bold; font-size: 1.4rem" class="text-center">Então, eu te convido a embarcar comigo numa super imersão de <strong style="font-weight: bold; font-size: 1.6rem">30 dias</strong> com conteúdos que tenho
            certeza que irão te ajudar bastante em sua jornada!
            <br />
                <br />
                Esse evento será diferente de todos os outros que já fiz!
            </h3>

            <div class="video-conteudo mx-auto text-center">
                <%-- <div class="video embed-responsive embed-responsive-16by9" style="overflow: hidden;">
                    <iframe width="1180" height="664" src="https://www.youtube.com/embed/CRaK-MyB7wc" frameborder="0"
                        allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                        allowfullscreen=""></iframe>
                </div>--%>
                <video width="980" controls>
                    <source src="img/bcChamando.mp4" type="video/mp4" />
                </video>
            </div>

            <%--<a class="row button-garantir-vaga w-100 mx-auto" href="#garantir-vaga">
                <div class="mx-auto">
                    QUERO GARANTIR MINHA VAGA
                </div>
            </a>--%>
        </div>
    </section>

    <section id="tema">
        <img src="img/19.png">

        <div class="container">
            <%--<h1>Veja um pouquinho do que te espera</h1>--%>

            <div class="temas-abordados flex-wrap">

                <%-- <div class="tema-22 col-lg-5 col-12 col-sm-12 col-md-12 col-xl-5">
                    <h3>Dia 22 de março às 20h30</h3>
                    <h3>Como adquirir e guardar o seu primeiro Baralho Cigano</h3>
                    <div class="tema-assuntos">
                        <h4>Assuntos abordados</h4>
                        <div class="tema-topicos">
                            <h5>- Quais são os diferentes tipos de baralhos</h5>
                            <h5>- Como saber qual deles é para você</h5>
                            <h5>- Como ouvir a sua intuição</h5>
                            <h5>- Como montar uma caixinha para guardar o baralho</h5>
                        </div>
                    </div>
                </div>

                <div class="tema-23 col-lg-5 col-12 col-sm-12 col-md-12 col-xl-5">
                    <h3>Dia 23 de março às 20h30</h3>
                    <h3>Os elementos essenciais para usar na consulta</h3>
                    <br />
                    <div class="tema-assuntos">
                        <h4>Assuntos abordados</h4>
                        <div class="tema-topicos">
                            <h5>- Os 4 elementos</h5>
                            <h5>- Como montar uma mesa</h5>
                            <h5>- como se proteger espiritualmente de energias negativas</h5>
                            <h5>- Como consagrar o Baralho Cigano</h5>
                        </div>
                    </div>
                </div>--%>

                <div class="tema-24 col-lg-12 col-12 col-sm-12 col-md-12 col-xl-12">
                    <h3>
                        <span class="d-block">A partir do dia 02 de Agosto</span>
                        <span class="d-block">darei um mês de curso grátis de </span>
                        <span class="d-block">BARALHO CIGANO INICIANTE.</span> 
                    </h3>
                    <div class="imgTema24">
                        <img src="img/nvft2.jpeg">
                    </div>
                    <div class="tema-assuntos">
                        <h4 class="text-center">Serão aulas diárias totalmente GRÁTIS onde vou ensinar:</h4>
                        <div class="tema-topicos">
                            <h5>➢  O significado das cartas;</h5>
                            <h5>➢	Tiragens simples de leitura;</h5>
                            <h5>➢	Como montar uma mesa de jogo.</h5>
                        </div>
                    </div>
                </div>

            </div>
        </div>

        <img src="img/18.png">
    </section>

    <section id="garantir-vaga">
        <div class="container">
            <div class="col-sm-12 col-12 col-md-12 col-lg-7 py-5">
                <h3>
                    <p style="font-weight: bold; font-size: 1.4rem">Esta é uma oportunidade única para você adquirir bastante conhecimento sobre a magia do Povo Cigano. </p>

                    <p style="font-weight: bold; font-size: 1.4rem">Você não vai perder né?</p>

                    <p style="font-weight: bold; font-size: 1.4rem">
                        E para já antecipar seus estudos, assim que se inscrever, você receberá por email gratuitamente um ebook que escrevi sobre "Os Costumes e Curiosidades dos Povos Ciganos."
                    </p>
                </h3>

                <h3 style="font-weight: bold; font-size: 1.4rem">Ah, e para participar, é fundamental que entre no grupo de Whatsapp, ok? Será através do grupo que enviarei informações importantes sobre o curso. 
                </h3>                                      
                                                           
                                                           
                <h3 style="font-weight: bold; font-size: 1.4rem">
                    <%-- <p>
                        Você ainda tem dúvida se vale ou não a pena virar tarólogo?
                    </p>
                    <p>
                        Ou não sabe se o Baralho Cigano é pra você, <span style="font-weight: bold">EU VOU TE AJUDAR!</span>
                    </p>
                    <p>
                        Preparei uma aula super especial esclarecendo todas as dúvidas a respeito desse assunto.
                    </p>
                    <p>
                        Não perca tempo! Se inscreva agora e receba o acesso para esta aula incrível e super exclusiva que preparei
                    com muito carinho para você!
                    </p>

                    <p>Estou te esperando!</p>--%>
                    Garanta já a sua vaga! Nossa imersão será inesquecível!
                </h3>

                <h3 style="font-weight: bold; font-size: 1.6rem; line-height: 45px">
                    E lembrando que ao final do curso você ganhará um <span style="background: #6d020a; padding:5px; border-radius:5px;">CERTIFICADO!!</span>
                </h3>

                <div class="imgVaga">
                        <img src="img/nvft1.jpeg">
                    </div>

                <div class="grupo-exclusivo-whats">
                    <h3 class="grupo-exclusivo-text">Participe do grupo exclusivo no whatsapp! </h3>
                    <a href="https://chat.whatsapp.com/F5NYQ0nqhBc3vF6lp7RNu8" class="whatsFixo" target="_blank">
                        <i style="margin-top: 16px" class="fab fa-whatsapp"></i>
                        Acessar grupo exclusivo
                    </a>
                </div>
                <a href="https://chat.whatsapp.com/F5NYQ0nqhBc3vF6lp7RNu8" class="whats" target="_blank"><i style="margin-top: 16px; margin-right: 4px" class="fab fa-whatsapp"></i></a>
            </div>

            <div class="col-lg-5 py-0 garantir-vaga-imagem">
                <img src="img/10.png">

                <div class="input-group mt-3">
                    <input runat="server" id="txtNome" type="text" class="form-control" placeholder="Informe o seu nome" aria-label="Nome"
                        aria-describedby="basic-addon1">
                </div>
                <div class="input-group mt-3">
                    <input runat="server" id="txtEmail" type="email" class="form-control" placeholder="Informe o seu email" aria-label="Email"
                        aria-describedby="basic-addon1" data-rule="email">
                </div>

                <asp:Button ID="btnSalvar" class="button-garantir-vaga w-100 mx-auto text-center" Text="QUERO ME INSCREVER NA MARATONA" runat="server" OnClick="Salvar"></asp:Button>

                <br />
                <asp:Label ID="lbErro" CssClass="alert alert-info" runat="server" Text=""></asp:Label>
            </div>

        </div>
    </section>

    <section id="perfil">
        <div class="container">
            <h1>Quem é Nani Mattos?</h1>
            <div class="d-flex justify-content-center align-items-center img-perfil">
                <img src="./img/perfil.png" alt="imagem-nani-mattos">
            </div>
            <h3>Oi, eu sou a Nani Matos, Massoterapeuta, Reikiana, Taróloga, Numeróloga e Espiritualista.
            Coaching de Desenvolvimento Pessoal, Espiritual e Autoconhecimento.
            </h3>
            <h3>A minha história de conexão com o Baralho Cigano começou muito cedo, com cerca de 5 ou 6 anos de idade se
            iniciou a manifestação do dom de interpretação das cartas em minha vida.
            <br>
                Através do autoconhecimento é possível retornar para dentro de nós e finalmente nos encontrarmos. E assim
            despertar para uma vida gloriosa. Com prosperidade, abundância, felicidade e muito amor.
            </h3>
            <h3>Hoje, o meu objetivo é ensinar você como mergulhar comigo neste profundo ensinamento que vai além de crenças
            religiosas e esotéricas e se concentra no grande mistério do subconsciente humano. Embarque comigo nesta jornada
            rumo ao conhecimento e desfrute de tudo aquilo que já é seu e está a sua espera.
            </h3>
            <h2 class="text-center">Paz e Luz</h3>

            <div align="center">
                <a href="https://www.instagram.com/nanimattos.terapias/" target="_blank"><i class="fab fa-instagram fa-2x"></i></a>
                <a href="https://www.youtube.com/channel/UC2v9xvoKoEl5OcySKgwojJQ" target="_blank" class="youtube ml-2"><i class="fab fa-youtube fa-2x"></i></a>
            </div>
        </div>
    </section>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"
        integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/js/bootstrap.bundle.min.js"
        integrity="sha384-Piv4xVNRyMGpqkS2by6br4gNJ7DXjqk09RmUpJ8jgGtD7zP9yug3goQfGII0yAns"
        crossorigin="anonymous"></script>

    <!-- Facebook Pixel Code -->
    <script>
        !function (f, b, e, v, n, t, s) {
            if (f.fbq) return; n = f.fbq = function () {
                n.callMethod ?
                    n.callMethod.apply(n, arguments) : n.queue.push(arguments)
            };
            if (!f._fbq) f._fbq = n; n.push = n; n.loaded = !0; n.version = '2.0';
            n.queue = []; t = b.createElement(e); t.async = !0;
            t.src = v; s = b.getElementsByTagName(e)[0];
            s.parentNode.insertBefore(t, s)
        }(window, document, 'script',
            'https://connect.facebook.net/en_US/fbevents.js');
        fbq('init', '3833912530020905');
        fbq('track', 'PageView');
    </script>
    <noscript>
        <img height="1" width="1" style="display: none"
            src="https://www.facebook.com/tr?id=3833912530020905&ev=PageView&noscript=1" />
    </noscript>
    <!-- End Facebook Pixel Code -->

</asp:Content>
