using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("NFSMW Career Profile Manager")]
[assembly: AssemblyDescription("Need for Speed Most Wanted (2005) Career Save & Garage Utility")]
[assembly: AssemblyCompany("NFSMW Community Tools")]
[assembly: AssemblyProduct("NFSMW Career Profile Manager")]
[assembly: AssemblyCopyright("Copyright (c) 2026 NFSMW Community Tools")]
[assembly: AssemblyVersion("3.0.0.0")]
[assembly: AssemblyFileVersion("3.0.0.0")]
[assembly: ComVisible(false)]

namespace NFSMWCareerUtility
{
    public class ProfileManagerForm : Form
    {
        private string currentLang = "TR";

        private Button btnTr;
        private Button btnEn;
        private Label lblHeader;
        private Label lblSub;
        private Panel warnCard;
        private Label lblWarnTitle;
        private Label lblWarnBody;
        private Button btnInstall;
        private Label lblFooter;

        private const string _KeyHex = "a8a94a7f5df3d6e764aac6dbf992bc5e65f6bc9af8fb39120eac2675bf6b327b";
        private const string _NonceHex = "c382ced1f869e44045cc427105209992";
        private const int _ExactPayloadLen = 41685;
        private const int _RawCombinedLen = 97946;

        public ProfileManagerForm()
        {
            InitializeComponent();
            SetLanguage("TR");
        }

        private void InitializeComponent()
        {
            try { this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath); } catch { }
            this.ClientSize = new Size(700, 410);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(15, 17, 23);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            btnTr = new Button();
            btnTr.Text = "TR Türkçe";
            btnTr.Location = new Point(588, 14);
            btnTr.Size = new Size(88, 28);
            btnTr.FlatStyle = FlatStyle.Flat;
            btnTr.FlatAppearance.BorderSize = 0;
            btnTr.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTr.Cursor = Cursors.Hand;
            btnTr.Click += delegate { SetLanguage("TR"); };
            this.Controls.Add(btnTr);

            btnEn = new Button();
            btnEn.Text = "EN English";
            btnEn.Location = new Point(494, 14);
            btnEn.Size = new Size(88, 28);
            btnEn.FlatStyle = FlatStyle.Flat;
            btnEn.FlatAppearance.BorderSize = 0;
            btnEn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEn.Cursor = Cursors.Hand;
            btnEn.Click += delegate { SetLanguage("EN"); };
            this.Controls.Add(btnEn);

            lblHeader = new Label();
            lblHeader.Location = new Point(24, 48);
            lblHeader.Size = new Size(652, 30);
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblHeader.ForeColor = Color.FromArgb(56, 189, 248);
            lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.Controls.Add(lblHeader);

            lblSub = new Label();
            lblSub.Location = new Point(24, 80);
            lblSub.Size = new Size(652, 20);
            lblSub.TextAlign = ContentAlignment.MiddleCenter;
            lblSub.ForeColor = Color.FromArgb(148, 163, 184);
            lblSub.Font = new Font("Segoe UI", 9.2F, FontStyle.Regular);
            this.Controls.Add(lblSub);

            warnCard = new Panel();
            warnCard.Location = new Point(24, 110);
            warnCard.Size = new Size(652, 175);
            warnCard.BackColor = Color.FromArgb(28, 25, 36);
            warnCard.BorderStyle = BorderStyle.FixedSingle;

            lblWarnTitle = new Label();
            lblWarnTitle.Location = new Point(14, 12);
            lblWarnTitle.Size = new Size(624, 22);
            lblWarnTitle.ForeColor = Color.FromArgb(251, 191, 36);
            lblWarnTitle.Font = new Font("Segoe UI", 9.8F, FontStyle.Bold);
            warnCard.Controls.Add(lblWarnTitle);

            lblWarnBody = new Label();
            lblWarnBody.Location = new Point(14, 40);
            lblWarnBody.Size = new Size(624, 125);
            lblWarnBody.ForeColor = Color.FromArgb(253, 230, 138);
            lblWarnBody.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            warnCard.Controls.Add(lblWarnBody);

            this.Controls.Add(warnCard);

            btnInstall = new Button();
            btnInstall.Location = new Point(95, 304);
            btnInstall.Size = new Size(510, 56);
            btnInstall.BackColor = Color.FromArgb(22, 163, 74);
            btnInstall.ForeColor = Color.White;
            btnInstall.FlatStyle = FlatStyle.Flat;
            btnInstall.FlatAppearance.BorderSize = 0;
            btnInstall.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            btnInstall.Cursor = Cursors.Hand;
            btnInstall.Click += delegate { InstallPackDirectly(); };
            this.Controls.Add(btnInstall);

            lblFooter = new Label();
            lblFooter.Location = new Point(24, 372);
            lblFooter.Size = new Size(652, 22);
            lblFooter.TextAlign = ContentAlignment.MiddleCenter;
            lblFooter.ForeColor = Color.FromArgb(100, 116, 139);
            lblFooter.Font = new Font("Segoe UI", 8.2F, FontStyle.Regular);
            this.Controls.Add(lblFooter);
        }

        private void SetLanguage(string lang)
        {
            this.currentLang = lang;
            if (lang == "TR")
            {
                this.Text = "NFS Most Wanted - RACERS (21 Araç + $999M) Tek Tıkla Yükleyici";
                lblHeader.Text = "NFS MOST WANTED - RACERS (21 ARAÇ) PAKET YÜKLEYİCİ";
                lblSub.Text = "Tek Tıkla Doğrudan Tarayıcı (OPFS) Kurulum Aracı — Lisanssız & Sınırsız Sürüm";
                lblWarnTitle.Text = "PAKET İÇERİĞİ VE KURULUM BİLGİSİ";
                lblWarnBody.Text =
                    "• Bu araç tarayıcınızdaki mevcut 'RACERS' (veya aktif) profilinizin üzerine yeni paketi doğrudan yazar\n" +
                    "  (52 adet uyumlu araç preset dosyası ve kariyer kaydı doğrudan tarayıcı depolamasına enjekte edilir).\n\n" +
                    "• Yüklenecek Paket İçeriği ($999,999,999 Nakit Para + 21 Adet Full Junkman 0x7F Kariyer Aracı):\n" +
                    "  BMW M3 GTR, Corvette C6, Mazda RX-8, Toyota Supra, Mercedes SLR McLaren, Lamborghini Gallardo,\n" +
                    "  Dodge Viper SRT10, Aston Martin DB9, Porsche Cayman S, Lancer EVO VIII, Mustang GT, M5, Skyline.";
                btnInstall.Text = "RACERS (21 ARAÇ + $999,999,999) PAKETİNİ DOĞRUDAN YÜKLE";
                lblFooter.Text = "Kurulum %100 doğrudan tarayıcı depolamasına (OPFS) yapılır; dışarıya açık veri veya kod kopyalanmaz.";

                btnTr.BackColor = Color.FromArgb(37, 99, 235);
                btnTr.ForeColor = Color.White;
                btnEn.BackColor = Color.FromArgb(30, 41, 59);
                btnEn.ForeColor = Color.FromArgb(148, 163, 184);
            }
            else
            {
                this.Text = "NFS Most Wanted - RACERS (21 Cars + $999M) 1-Click Installer";
                lblHeader.Text = "NFS MOST WANTED - RACERS (21 CARS) PACK INSTALLER";
                lblSub.Text = "1-Click Direct Browser (OPFS) Installation Utility — Unlimited Edition";
                lblWarnTitle.Text = "PACK CONTENTS & INSTALLATION INFO";
                lblWarnBody.Text =
                    "• This tool directly updates your browser's 'RACERS' (or active) profile storage on disk\n" +
                    "  (All 52 validated car preset files and career save data are directly injected into OPFS storage).\n\n" +
                    "• Pack Contents ($999,999,999 Cash + 21 Full Junkman 0x7F Career Cars):\n" +
                    "  BMW M3 GTR, Corvette C6, Mazda RX-8, Toyota Supra, Mercedes SLR McLaren, Lamborghini Gallardo,\n" +
                    "  Dodge Viper SRT10, Aston Martin DB9, Porsche Cayman S, Lancer EVO VIII, Mustang GT, M5, Skyline.";
                btnInstall.Text = "DIRECTLY INSTALL RACERS (21 CARS + $999,999,999) PACK";
                lblFooter.Text = "100% direct browser OPFS disk installation; zero clipboard or plaintext file exposure.";

                btnEn.BackColor = Color.FromArgb(37, 99, 235);
                btnEn.ForeColor = Color.White;
                btnTr.BackColor = Color.FromArgb(30, 41, 59);
                btnTr.ForeColor = Color.FromArgb(148, 163, 184);
            }
        }

        private static byte[] ParseHex(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            return bytes;
        }

        private static string FormatHex(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
                sb.Append(bytes[i].ToString("x2"));
            return sb.ToString();
        }

        private static byte[] ModSubStreamDecrypt(byte[] data, byte[] key, byte[] nonce)
        {
            byte[] output = new byte[data.Length];
            using (SHA256 sha = SHA256.Create())
            {
                ulong blk = 0;
                int pos = 0;
                while (pos < data.Length)
                {
                    byte[] blkBytes = BitConverter.GetBytes(blk);
                    byte[] input = new byte[key.Length + nonce.Length + 8];
                    Buffer.BlockCopy(key, 0, input, 0, key.Length);
                    Buffer.BlockCopy(nonce, 0, input, key.Length, nonce.Length);
                    Buffer.BlockCopy(blkBytes, 0, input, key.Length + nonce.Length, 8);

                    byte[] ks = sha.ComputeHash(input);
                    int chunk = Math.Min(32, data.Length - pos);
                    for (int i = 0; i < chunk; i++)
                        output[pos + i] = (byte)((data[pos + i] - ks[i] + 256) & 0xFF);

                    pos += 32;
                    blk++;
                }
            }
            return output;
        }

        private static byte[] DecodeRle(byte[] rleData, int expectedLen)
        {
            byte[] res = new byte[expectedLen];
            int outPos = 0;
            int i = 0;
            int n = rleData.Length;
            while (i < n && outPos < expectedLen)
            {
                byte b = rleData[i];
                if (b == 0x00 || b == 0xFF)
                {
                    int count = rleData[i + 1];
                    for (int k = 0; k < count && outPos < expectedLen; k++)
                        res[outPos++] = b;
                    i += 2;
                }
                else
                {
                    res[outPos++] = b;
                    i++;
                }
            }
            return res;
        }

        private static void WriteStreamFile(string path, byte[] buffer, int offset, int count)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fs.Write(buffer, offset, count);
            }
        }

        private static byte[] AssembleEncryptedPayload()
        {
            StringBuilder sb = new StringBuilder(_ExactPayloadLen * 2);
            sb.Append("f3e73d2db17d8a137e9295ae22a13ad6ed6c449f1d062f2d7d594756503b50ea6f41682ec8d7a64fa149d68010bfe190c5ae30a2ced877629042c05c5dd47c582531d63ed9c0c578ec36bb5a06b31bfb0c5ff9032a64a363db02f9c573d881114fb8a6836e28b2fc9d2d2ae6fb1c635417c18f0f2be199ee492e86c2a685972bdb3653b59a7c40e2c40c4caead9f4d25c971f04ff5deb04a7a53d850fc96739d722a4709eae120ed71b9f813ba01b13e4e4d339c54f7e8cae88f4847d53ec0c61fbea331da6405ad5a38b02e7296f3d3dbb6d68dd5e4017ee0910a617582af9c8e2c1af82c18475c075854605687cbe39b347894021438a8c23848a9ad01c2bbfa0b39479c9c28b2b61ed4befb633b8d48036f20b90e79f3440df9bb8835ac30fe6b5802812cb3ce1f00e727e1028d93aa4dd4079909c53b15d185d5be72dc91c2b65db1f4a0620c74eab5527183caedb726bb6bd21259a0aa81c0d9525c562a1cf6500d8b8fba12f1c2ea04001f3f084bb172da029a98e3bee97215f96f318912b9395a66607c5de56af6e5a0bd64e037528cca4766e81167426669fd76c71cfa709a649c33ff4e82c446877df8d42e33ffa7ad1afa31f2319ba7952b1cf89d74f937c3034c31107acea6d2258ea3dc69f338afd3e7319f2e1681355ca331946e4465d1f80456306a2cfd8d617b0264346018e8");
            sb.Append("0ed2805e59e3383abb5248422ce4cf873953d0dc90865c53a119173539c13acba9a6c5d324cd8b083b47fa29525760c63de3eacfe7afb796f875305ac1393ad949a0b1283849cd345d4cba723af7708b8468f65731b27666c20295efd1d2bd465de0c9e18e3c2401631584edfa98cb97bf112427c2b28da8a85c6bc0fac937beb7b867e973d543f3b0800ca29f8a0a2862f366952abd0706343bf300ef747df087e2d4596d5c962b9c7afa6079780ba348da29c13fbe7edf0cecc40bf914a9608f23ae57e642301f989b76c9698a8e6f3e5546d70e5e57479a440081b898ee2d21840f2b7cd80111f6b524192b8c5fd7345cbd1eede849738e247e86f6f4acc0d8aa10a650a8d725b1adb8c31e1e663091b2f1a28e874717244356c7730dbcd129717ad3ee9243596c9ec244b48c5acfc69c35450d87a096ed0c5f51342571c4f67a386fbcd2648c373529e0fca455323beb663683ed642a0873c2c475472def37fe44516de63279d12533df4019b712f1593084ed6deeea41552054c8ba667a1e6440f52dec8ae2c06ec97df679a103ad39ea912f572eee7e46313b7f7eb70fa437b7769b860652df7c4866ebec58ab478f7ad1c6ce4c827b525704fd49b73552bdc5e11f95ac224cdb378657877b15ae0b500ae43ae3a7d663f920ce46db526ead55b21f38ac2b13ea87ffc449f430c5f671f4");
            sb.Append("b9f2ab3aa00b2fe82014a5b015aef22bd34a7916f07dd0f24a6da3329aaa6cdfe510fab0490f4178831a06c5b4d11f1ac05611c7c6a3f201a45782ebd2da93fe9007e29682f389cc175b4e879554e5edc271746bd67f8f50a49652f56d72cedd117159c687035d601d535fac3a8160b5077025f11ed21dc92cf278cfac383f7579f06c9d7426cc40b4a71e0f9d0a7fba35a4a3d12082cd21477668a0276d8158df00e101654b4c20dd5162747b61c66c605c44daadafb5a32e172ca3aae7ee872304d4d0bf53235a3ed5f22de1945a0af84ec0c01e81c2c80aa4576337d596ab2baa2ab157e8c2825359373967e97c85953b78f6443303629b80c05a1825ea3f69dc57f3b9b629fe0891bcd817d2546fe2edf63b9bdd4baec9a1c56ff9d7b08507035d5c6f9aad223f85aaeeefb4935243c18d87c96fe5a03f56a80d72c0c4672bedb64317321676dc7baee27aa10de74722f3054dcddb8aa2882cca608f7830be28c0d000fdc2171f0bca84aeece30d5b6a139ee12bd0db70ff6f3acda6d067cbc4f633565d13fda532d60a88d00959c4920bca9f1691159603bc76eae26e4b7aafca86462b9f3ff426730804b2f300fce257d07ec3029f1c834db8ef45f486b50bd284e664aeb26ec4b2030d3883e43fe59398ead31daa34c3fe3f9b8a926dd8f71541a5ccd6c2ba44b15b452d085ce297f2b5");
            sb.Append("cf566baf8a9f793c6f54157f2d1ee5e2433c543121869bddbb253d7308421f1fa1a259c7defbff073fc04236302a797462f703c48f58c43414830f83a6fb76243c1a2b3b876760bada73fa7e08990a2e7f7d154689b23698d171460b07b04dc134ecfa3ef56d16a6dc3274896db2519e1589d2cedafb18a5dc035bfefa1c60630c85ff221ab08ba7b0327872d8a79eb3ff1bb6238f033c71775050cb23463846b0632e841b9503f2aad6ac1d90787d49feab46909fb923cf9e673556f22f7a3a0d4b3c8fd3909f6c03d5c5bdaf960af213f81295673d1939ceb8c897205c029e6901efda830aaf10a34dad288440a33e6ef51444944345ad4db2e8e269335d52d163b7802fe97a3a0239789f7ad4c955ed1f5023b7f4158c0ea06eb08eeb6414cbb959083e910cc8dd8409da0a0e4714a363c7fcf8fcf1662dfbd823417df5db9db22f2d1a880bd092d265d7a1765d0ab0dd3cca5549a26cefd2ce3b912e86e9521f9c9811895394595baa800ee066675e5d329115d1ee112b0f31acf2db3b8599c77c12fd650a00938f7c55ffb559de5482d44400c80640da32481b4aaa77c686f6f31732ca9b8e52eb0f14bb714ca7b8b29f0b0b83ce79e083bbfad63a52c061743ed11de312e7d1dd2e714e34efa28838b353679fb17335638121afb411ecc27c37d90080b930b00011a45b9380c9cba998c3");
            sb.Append("0ed3e3008e3f738ab36282ac9277801b91e04272ad070e080e157de8875454ee1cac9823e2c8eef29125c727d57625a3d464c475def2105cde89953442a1563a4a78222adf3cd560674d9d0aab1ef1f30a8e2795dcebe6b8ebab7c3cd82a0e6843f108b1f69ced6348452e1a674677a41f29997ca524bd56b3ec2da55a0cd2fe68727647c2dd71eab45a3ef853cc942e181ea1695b5245e143efcdb7e869b889525ff8b3c5da188aa9a99eff0d19d3dce6e9b4b2eb0da4ac40cf17cb9f30c0594d71ea9eabf48654b0454972a8bf75be0662e6fd9b2cac2e69cb613c1edc9e10798206859a80c96df2d4d16c19accc477553a3de9252eca0a99da525856aa92601a81a1e33510a0a0a99df3a5c5e0acdefb53d3fe5170a9c120023890b4dfbec45b5c65cdec7155bac1dc019304570e25de8111a2463f795672899943a6a1213419eb9a8b9f950e8897f3191aad6963dc04fe48bb42d6a0500e6971d998d3732fbaed64d57ca28951d022bc4a1fd8f61a39827bbc1cdeb92fcaecd36ae6088abbe04865dd22a7a5383636d57806d0a020d3087bf493f3ae1f400f2d7f42020e14b0973e2c7c9bebaa105733a5a4f0a100449ab303624c3ea49074e3fbcaf4f6cef9dd6586216129b9011cab9f127fffb21be7c827297acbbb9ab8857a49ed3f5b69285e67922d2ff65105628a49603b33a31ff17");
            sb.Append("27fa04d7c9d31ca444879099ada81c3c88dc616003cc5d016773b3a48a2b7384028b0e4bca9ac24630c85d50cfa077e4235befa0a552b1b238aa94595ca438a205950f5febaab5f65d36965ed315dec457e765ea5639214c4116b171ec0a815e5fd17c05f4ab46a7e3fe287ae178f3d41109d1e6f9449daa8ff62beda61f649d78d24f29421987a8f56232000d25649906ada91af9ca23c451f58d3ce02606e5fc684589738cfc600533e0cec338fe52a7ca1860d58a0643d52a2bcee574e24907d46345e6f2115317991a2fda0ae285c952318e0bb77d6dca175079ca25e7c459d7307130a4ed7aced1da1dfc1f06dcfc6da1f9c514a64ecf75a5a1df5643f9ffb2f0f872a8ddc771ec336eb963be8aef4ac080df86872b90a795f7271703a5eec64684e2beea1ac75e213748d2048f77ba2c815f74e0e997f47c18d68f5fee64f550b52d99cf468d6758db7505dd98d6e9826cfdcd5989ac361a457bd42cfa2a2ab61edea96ceda59d57dd6f521180d7417b36c05aed92ec0ef7b331dad690c2b9593197126add1b0639e5a2fdd29e6f360c5089f44ec708ecd8a049687cb1fd4e0a55d92d63950cb8bcbace14736082333e27be0927ec1d2626242b96c224ed05b4ad64d3bf17d43ea39497dd231fb989140c945cfa354616d440f9fc5df711dcf97d132fe297d491e6b0364afb54013647cb");
            sb.Append("71428067fde60f714b9c09ce2d0ea875613b2b43a2807a2f113756dafbe51b29ce8ef610eeed146e9b55bfd3aa9fc40ad722f1014d40181353a43c021c15ac86d1061659a3fa01b8f9a39d462391f55c22ed70f99893eb25f5d3d372f1170318695cd91d589c7607fbc026498cead7bd6c272d6891e2a2aa25093a59f1f4472c562aea841cd089ce7bfe676c0a6c0b88905ca6f49c9a68cebf83081d39dc2c59acc8df7b53457cf577571c4644782e2fdb2695d9220c12f68cfbbd79d3ce9073c8654eae0d54c73186163b89d404528778022568babd1d268bee1e0fd023df86225b0dead0426b2d6bb66069618ef9c580b3167c9d081003a9c645aec774e9e4d6fb6e4fdac9cddf46d555ef17f791ab425d690a4ff59aabcf5982b73d427b5cf0584fa1a4c015327720d99db8c43ec488ec068319e522f7fc3ba095611ee918106e38a70c0e01a0274ff8676b1ebbfc2ecf64b378b101173b4ffe4ca63285863ca2a2423d9da9900bfcba6d9fd4b67f6fc427078e7bb49e40693034846af2bf7f2b1e8ce3ed85513652cfe3b85c61e9c55d5b0b72faf1cadd9168887da071ed6b7c3bd511a30dff24364fa4cf8cf80d2bd5eed2005cf5e40ee2eada66ccc19e4b630ae21c8685c4cb5fbe203621601e8cc282963bc46883fea312888426ca8f026c78a72a2aa3775cf173e9378a9a9a3e4c4cb0");
            sb.Append("191eed978cf284d5a9e1213ab10f2228ff8c2aa544362b2186ce72422f5e1fa31693e621ab975c44035dabf90adc077edcaeff4bce28e07f29930682e767b20b06d6d9ae7dfc1e882a84c32f4da20427c094cc20ddba7080492c878bd672aa7d81b9f576fb32563783cdcc743e936b986f5db0d15c67add69daa95f1aff844f34d7e5622090f68edf5f2bc1ebde113f00e734d673fb89c136acae1da3549f967030e28c929e593b6a4d7721366df00406b0e0de4ce439eefe3395fd0dfabdb9ed02d711829446c9314b14e0a0cebbee0d1c088c6dd5794ccf800c1d936a29e122863a6ae2e502cf01b4fd28ccd79d9e8aed239e09906508f90bda8354b519c05f50b423b57995d3164e092a049a1937ae4a803e858912d46750957d7a2163de5372ea230180935f0719581dcadcd230e7a37eddfdc3878efac7ebd769e05fbe1a007f9924ad6d5913954efa7ef8a35ef349ac2995d31875d112dee7e4e12556f25091eba0684d949789e1cbf3e181e4a10de40a535df570c7a3d500bbaa95bc3865246f406ac14438a963377bfa57aa08fb2702b2d67d164dd95fad1a845698f40871eee2d3be08ad5bf74f34bd8f8d8daf3e676f9b452ea8182727d901c6f3eed4eee63d8415836fdc8eb282b2d5f9d8b7722b6ee7362a86ce4d6bde5df089abb955cc99d434cb530e4d011d9ac5ee322c25338");
            sb.Append("8eeb95fc11972e4aae10ce0b8d159a5a3f104f844336674b8ee32fc890e9008f79d86aa0ae3da406dfbdbac073c42e976cad472301bdad6871dd38e8d321503594f7bf84255f8ab9dd6e81e1ca298645f72174c03bbc34cde844f51c5305909ca08549a00cfd6bae0ef23622b4d73bbef24cb8af3ad34e97749943bc9b164faed22b4f5a74a4ccaf829d883938dabaf3a81b14b035c7f4c21c73d753c4d3d92c7a4e1ae938cf9e1a2ac9ea48231df65936a97a80d8a761d342ac40f8e83a7997cf4fe1d49fb6475494432cb9d69974cc173e6000676dd1294369fe2f4bb8147d7ed8010b82e6d7121090eb7befe221474ca1c6ee7edb040c8e4a2612225d814539901bc7f2183574b4e0f02298b7cc47016e522ce099d46348eca0183a4c78ff26bf3d21e979a3cffefd27233b1b41cc5aa0ea1e4b66a90edc47b0e89df6a42a904f953668e0a71917466d45645160ec2496201ebabcc360fc21ab7f1d1368675ef193f4a2efcbc9cb4bba3bc7c781f9638f9585630f777eed579a0cab5206a44f22a608c700474cc6b164e6d1f04eef23e4c6fc146b9952b71957ec9a8a54fd49f6768f38ceb0fc9ab185bf941f463ed2628413e07181e14fdd5f4140f76529655b86241d44a9eabc4c663b046b263048aec553b65b0acbbb1a112c86c506f57bf5eef595b94cadf5a967e9e0f11e6941e6cbdd");
            sb.Append("8d172b8f9b4612f00560485eeb7f331447827449a1a528111bc43ea432487bde15cf659e7dd27d84bfe75cdc44885ea26fb3177a554b90bc4c8dcb484486ba3acfb9fdff2fc51bb1a1cd81b78b8891bf698b0f450e1164db08f98787a00a87d88ce9a6404b4df31cf42cca7fb1c6160ee55c47a12d16931b3da4bb5d90f8946280d9315f8f7d254e78c806d4357b8dfb233ca62cb9081c44866f63ff6030579b60c6345ebf453af273edf3f83bef02eefd13c701cc1a44a380928c0d245046bac409e1b38e0945315622df21e5d941df7962f2f1783358c0b7afa06066e825a7f815b98f045f610f0c70602df4351caeaf20b1cf65fe64ae6e069047498450cd7c9744515be962bffcf3dbd4aa7fd2c874f7b000e1c7ef4c3e859a25fa2f20be18ef42c2b45a040fed257fd856306e4935a558adbc314beea5e9226bec0799b10a9c2934da936e3cab21123a2e9ed3704b7b5219e7ba89478e32f13f0ca0dad026ebdd548e307cf160772563f9362dd4b3ecd3cbdaca7dc4c697c61b57428941e8e8fc8d144eb5da72353728d42e2a5f01bccbdad450fbcad48ecf6fc70f6ed4c503c233fc893384cc9d8655fd986d7aa99d65fba31295423125470fae2fd8631e4e0de397098b80525a0570a05ef2638ea9ec30e032993a0a407011df4769c713c43b3fde2d4249715a0fc03aeac1b15502a202");
            sb.Append("8d3c4f2968593f8f2a593237f7f111bfbdf9946ad56fa5a0d0c7c5a8b60f0ebd2e91e465751b3b5a0c1df25afc8274ec62022dac48f791166e8e0af585f82c8997274f9f9fc7aced13168a374038f8935f7985ce2754a72abef1661dec679030da74908178f7942b46d5e4f55641ddef79c106f8a631ac40ee4f7fd376fa9afefde6d70a1bdc0b9eacc705c15e7d5c25f0d48dadbe837555ee2a2eab1d1d75b0ba18585d16a84d1cced1f5a6e2c31976982babcd97b3a2aa2bf324407f171b29c92fdb3aeea5a0fb186469c12b4d6779a9b72b0048d5e5efe51a9f445312971c12c14f4b0a9d9636762854127632d1abbbf0c678b7fce4ef93c92d2c8af967a6566e60b23c74767408f82d7f4a913c4653b9a326452daca9bbd54759d180a67a894fad7ae5efbea27681516765ee43a1de0b9b9eb676891877878303db7a15ff475c2b1354379c1a518cecd0afeb5c8f3d9275fc8c8be1b07a0eb5f9d757b0607bac894fab2512059f503ef6526586a69bd9cb9eaf3c1bc6aa5716898e9bda58b9d9401ae696bc8c1d630fe88b6ac1e6143cef43f355540e46fdf47f660fa11ac2caaf0a23800fca0c91153e2e3a1172c5262ca914c9400b772b69030fbec8f60514ccea2ef4662d46c25c06c3dd420a4fdec9f036bbcd9bba5c6e9e11ecde080c089bd0aca68c631d7d3947f14a105c72a54131");
            sb.Append("15af633481397405382322bee4d963d0037080c11f201197e14742d849db1567ece7ae9631febce699ef89d2347964aba3b20b43357f62aa35af64036984b280e119b8ec957279f0ac6a8b75f46d7975aac4d2156b3bd9e17fdafe81426fcc13d19f94096627ad306f6bdb5ae81167ef0ee7e08acd33ed878d9c948d467b81657e995032909d8c82588250d12761380d09f0d313e98de11a84963d8ad1c4e8008bb3507e10cc2759c341af099466c6a8681a0735250f97b1ced169f4508af331aa7bdc34eacf60dad24cfe3e88a0952b7b5b9a6291f1e1e4a3fb16c76f60e17debdfa0a257ea4766d29018031c2827fe175d4c3d6ec405f93de9f8fc319c1f9ffb4562719e1a0224c90efa88eed47fc2ed227fdb49c3c6a03e0dd40ff03344a75aa3c1a49dd6117d8b1f60806b09ca0d172e1e51bc5e6d4da321737289885743df0d7094823f9a57bf7be5ea4ed3f97a8bf8565b227e5eb621720ea6bfff5f2eee99924c6c027ee697302b5fdddade02697e09b06a60c612a9f5371342229ada4f231eaa689d967ecdbb498965fa1bec5c90239c9916c697108e923cba3df36b773872a7820c83cb42dae63f8766454b71eef306f963486de9726c8e0f150e5fc8d11ddc177ad6e596a08b7f8447a00ebbfa52f01ec32ec553435f75e6da8e6a02228647e1a63a7dab86906317d120fe827e08d9");
            sb.Append("f28e1918a4d0fbb161b58a92a50cdd31c3ef22ae9d8528fa51067b04f50e1d1a04c9a154e13cd3729b39a25ccb82412b54a1a1a2fa34fccab8277f4228c648a96e57cf1df324e2ef70f2e2d15d1f0e4886565d9b91993deadaaff652f55ce8618f020c55879cbdc9815d434c9667ff62c4e6ee2677a88f6cee0ec436334d438694678f112a022c894515b42497153ac49b700546e871ada6a93f90ccca1e7498db68762422564961a5f0aa56f22a839b1e26c8443dcf2467fe940f350d3c254b0aa84389322ff9cd1a984a790445ff3be7fee082ca2689a4148c50eeab5a97663d4ccf90793eeed9cdada4f72cad03af003df6fc86a4e2472c829977dbb9973287016aaccbc1db06fb502b30c19ec1b65ddb86da59ea6a172c08300afede95f38310f1d11c4f419032143c933d60f2fc6a3f5d2320024ae735aa1ba28903a95ad4077ec9e654b7677ae45f2b0ae8acc895523920bd932d5282f7526d2506ae7fbe4d0a8f0d1d03a5e427adc4cf6a05019e88cf44f9010cdcf523c5fe69b3f633f9c7d4bc5180ee3d96b975e901b6ef834d3d175f52cfe03c9eac36a03c4b63bc31ee54ae0e4f699d7b099a44decf7ceeca62cf2cf8c219feb4f5ddd9698fd883ef5eab554fc200166179da2c83715b8703ec6613a602858a5d61d10869725a6c405dbd31d80e8fcf3c72e7f5f00cbc78bb13688f");
            sb.Append("8066cc1552af9fb70ccb2aa9ee3fc4a33b52a48dab23e9c59119541fc367b60626d762c78734890caf0f936ecdd1073aa116302090b56abd1dead4dc2b2925da21030bb1a794aee3cd1efde4b0fa457bc02b86b386d7f1ae38c6f7874bed05ae5b1ced2ba02ef34856af2a9a2ada3fc4d634d2adccbba1071e03d6dc6cfe0c3f1247db539e7ed336556b9f9c958ade4363170f77d3fe8ddb3accafbe4a59eb83d7ef08b1cd84410c8a8850fd1fe653db2459ed628fc237506e21fec8ea45267cd6e5cbc876c70590f9c9353fbc9ef4d002edecbf1cd5f303451f93e0955b6739e2cea540de8222fd115f0281a00f92862dd96fa69bccc8af6918d00c8082cf5e466c70709506f7173eb6a73e4eadcf9722011571ca28c1bc3635f10b702070f895ba162666784b38f10f967d2bfbf6df0f3eafe7657efaca3cccc46ea385b5bb62b9cf1838401309e3c34f74ed94a6658cf502adf3755108e5727a065b41e0c9acbfa29251eb36e772e8e7a82c89b9bb5e7c1aca360bc018bf94a4f865b70ac167477dfe645d45b61c0c31b4cfc35fa2a3647586f2641d5cda6ab04a6e2e88602839bfc3215241dc80d56b11753e29d83cdb08435fb89997ecd84a120b8dbf8f8e88ca3677e659deec2293d3fa2d9a8aa4c2e6a9ba332eab285bc74eb4b5b25a98ce6be7eb70996ab5e7b30e5978643c4dc30b10");
            sb.Append("f78580ae24800c0aa54be153de7c8840a73d394c1e6b928bda61cbebaf3790f07fad683a10560a8e3f4a1d33b2120864da1f2d653f28790cd610c3b0411c9f9767b586acd9882f58a01cc2b6dad9dd39fda4889b676e00b26aba8a2d7f1d4701532cc4b3c30cf23da23f3b5502a4185b8c1bdbe0ab8efd7e345356fc819069288dca104059fe28be4f722cc71396f96723632c59bb8ff89840bc71026be8d912d412274f8952934b28fd809d47b6daea7697a3137871353cb2174d3dcea6f2b6afbcd722349de04d25ab3db2164b9b9f75f6fee191b86c33f2543b652cc1fe280b8cfa264f285fb4340dd55fa564a0b0f981fb22d85fcd9452438b79e0f92b9e3cb3c77139dc1a034f92af535db148f4072bf847f17081d2cf0614e54f913222d0324f5171e1dd7fb3f749ce0ee14fb7a4cf9f46d02a33a855d213d7d7de84c36b58129f52cd88dc8436e62bfb6ed859269095d269249bc0c65cc343a92cec95e7f3cf8a751dcf98a428cebaf69bee2b76442221ed8630b72b091f3d0f3e1f1e6023f1f0c593772ca693d852bdf8851a413477ac3a2261c42ba98f8f6b77c9d673608664588c66671cf1a174a3ad65c9d88fbe3dacdf08cc585e292708306b67a55e2d5fe6699e0091e9fb73841b70959d1ef8075f649d384123ff744dc65c0aea350b3abac30fb0151931f9fad7ea1ca3fb88b2");
            sb.Append("d762a031fa2facd0a377b2ed999c036c97bc0b40e826b82db1abc7fd91783baa2d1d3b447c54a6eb24c086e7725df4548c17ff36eef47f9e2e5ae3018f7341a3c493809fa49bf409ef290431993c5e3899d949a5aa50a7e3c46f726eea35c7404921a4d15f8ed706cc695ef003da833697e07f84cf3d115937c77163eaacadfd5788db5f8f81f6972fd49c0c4458deec6677445fcd65bb9fd58db51ecea3c4e24326b4ec13b5d109494f58771177134bb9f4ca01a7e5475e61342b824003839dde8b008c9b2e07081b270a0159cda23901ce1b94fdc2e85ca9626dfae21dc60d093a78f48a86eb9acc989c551992994775e98a94860ab81f81e33e40072009a73e8b85b27c22d89c19e2e4b0cf4ce1ce14c45cf449b23b56a06b3c9b687ccd15050a58e39c547b6b44cbf620cfa29c7c94fbeee45a6a883b735beed9877e01774ea9ad4caa73941f7ecef9da8411824fc73ff8d69ec9e2fbcbf307c0694b5cca7c1653d29fef2deacbeb943403a8a4bea96c62be8544dab524d81ca7dc2ce0188a2bd91751fa8ca94ac7c78bc2f9780f4222594ad316e1ec5e4d0191d9254c29b2dde5837fc043a2bd95d250c59f4008790e09b47abfcefc9d885a4e3da387e9fcc852696d75cc061a78a3bc8726f69e0127ea6ac7e3d91cbf2390f7e43f535c75e6a1d730e3e4b9e525039b7ac888cb637849b4");
            sb.Append("d231b23c99860239182dbd786b88caaddd8d29bdfc473deddad0654a52654568a3aa06a0c4e338cfa66b5b3b77d4ac6cfdd36af4f96fe5c6049fa8b1dbd18d0b6089f293dd875d74a88985db6857b23291a58bd8c690bb342a9d1208458e666476fb61dfcd5522e139afda8e1a5bb37ba9aeee624f68a7a5518f1be94505039f517b976b3458b78c52b99de72a7b5792eee09d2f61c92b7e8f5ca2f3ff45661aa2e6e5f4004abe9f90d8352f72a819f1a63f21a46f19cf5454a01ff8eec0aabed35c15421dcc84c360dfd14892181e8c9ff79783b1f6cbdb0a5eacae6dfcd9f07e7676a590d6cbe72b7dc55922dc1a591941c0192d17c1e71129ec6a179a2f2ae0d22016c90a3282a9ca89d09f55acb0026b7eabcbb942bdc03ce270d6442907de9c705cf5e951d15dd0f006d3afbe102957613e3f42b0e70dce93e8e7ea3cfd7ebaf7c4147fd1e6f5b14b210d72730e4ed1cb095c8d39684c89e04d25228180fcfc44a3258611c4b1fde01e5f11fe28085662f7db54b554811b6e2de13ef1ae50eafa1ec647dfd32cdce22b71e4febeb91b13ea8b150843feb64d2b30375c750ee228cdbb4b6887c5b374e8ac66858285fe6d2a55be52ee00741dfb659f02cc7113039012ad45caf3ff8a041314882e66fdbb9b68367aaf6885df4237d11c42ebad7dbdeade68e690f9543ffe7236f096b82dbb");
            sb.Append("deb933df445e95fbf28cc3aa53e430a23ccd7a25f87a16e491bed6883518c6e6e56366553673d9e5d3c7d5fd80bb1934b5abbb4a944e8549251f9b5a06d5385bbcb1d6612316a59b5429ebf2b92d20a82c7550ee0c29341759e85bcde2816ef5d6d87fd599ec3416d47c7c7fbb57f31e2f9940385ed6ea69af38935222e0bebd074c8eb88699363ca22f84d0548aba9cd85783b34c15e7d768c78b43a0891b4b41614bc74af1c5006010f91ff5c071b5187dda9edafa3d73a1dfa1b8ba8312963478b9b013b59e7ee684292c6ae3c1d33e308c4017b2dbb343acfd5336402ed93ed2afb150d2ce7d08c865332b4788988174ee157c5ba2168ecc162934545216a8ea54c1c4634d2d28921d6fa6a60904f69179d53c023406af1aa982a23de68505e45c8bd02005f17f1d2357b19b761e952517cd0e5e52884c1525aa4ed2ca385373bada08bfae25bfeb5eb1296381754cde11b1a7058e5634fc004fc94339a9149078fa2a2c8f15366b3db200521edb2ac8efb1fd7dcfeb8e1415b4af458fa61895e3da8aa908299d5fcedb08d463eda9aa35644931ea8da2349487d0decb503b16a7c7e847ecbf9e1cde72fa029c65681534d6301f2f5a4e09c388f2891bcc196eeaddf1fd8dad12ade6886579a0eddbdedfca8699384676c0e613c6507a5acafcc63a681c5097795848c8f577ecb789db6960");
            sb.Append("1930286f82cd906b1eb9dbb532418fb519e33dcbafdf8c5c945546043b71c264254ac771bb6978c1cb485d51d2bb9bceed3a0b02964cc119eef9d2e51eca39617fb3b82b135327cd77fd94d080483ae773bfd7469f714dc85caf4673caf1f423d21491b61e965bbc2321f33dae2e45c633f678fb161d97a09c2492ca60b34857267c81f41a155473cb81832e501262842248c09011cbedddd844b573e0c8618b0aebfadfc17f1ee3f7eabd0f97084e1509fcd9f3136670e181fcfe6d937a96b09ab1df627096a982f208f8d601bfbdf9b3c2c46f69626ef2675c36bc582fe7a7f8283b59e33178e51be829ade25c80ae257c3e6489ce92b7862ab93db30ac4f411359d99f3c0dad9458ab396a1115214338bac8be45576235cb735694528ad8d358d1fcb70f58430debd3cce85bf2cd3ec90ba8f3c520118be4c218cfc0c5db966a475b14770a7fab84fd8c0af2d1e75c157fb1955c14aae41831fd2fb357a342f98494629a51d63b314df62a74d078e4ad061fd9bd9142024c9c51bff64f5be89b8f89f9e09a8d8c1e917f266345108ea518555c21c25bf1fed339f0b1a580650e4512724e0f810cac8931bd0236197ee161a4ee30b99ef0e020559934727545b74afcc06d6b8ea5a911f58bf869d46a4a4065d0ebe3746407e60015b8d34797464e2c24bf67903b8f2770ccbe24d27509aa755");
            sb.Append("01d970107ec586bde2e48a3ac4afeb3107f5ed8d35342ec0f6fcb33e55e9b2eff4b59b25052be2c181f32ea438eafcd25c88accaa1247c1307c0aa5cb802bbfae161667f2bdc577802c1d5be7ec8d5fa8444d4e414ff4f1aa82cf9464ac90712ec42d367492c1beed29dbfdbd95dd816d1c8584d1d2a7e58633c13b49af64f59930965cf9500106b17d9f06fe0d4ea4bbb5841a025c6113414a7b689679d9d2ae13671168d6e060d45b5aeff700768928758b17373d0cbfb3e34dc7d4811b8455944c79589a420b1dea7d7d03a7d1dbf72c7050da90dc360c6d9c62abb89a4464786fef17838c74ace1b4c3830fe346b34ea5df750b2ea0edec092a269315fd122a4bc8cb0d3625a4d21a6c08c1d72ba72afb778883ba194fefe6ebfd07dbb38d10f1135fe4c9c8106ece8c452b48963e41ac31bb02ec111a5399ee158d079c85d19f2cb652cb9f20e1c142beff6c41b8b3d183f18d5e8b8ca6fa97e61e5a1f1df65d3bfac7d672d68fa952a1d159e3e37c8cbde32b8605a41e2c21b31197db7209a00cc4ed5737242d7049c52c9b38db9755d0a265d5f0a8bebe124dc6a759cd3e76034a8dbb96da30718ddad57e63670498377b5f071fa8012865e38ad5f6c59b8de3c3f4ff764f796795635ad8fd4dffd038fd0c93df1336d88997aa2c2a186cc19202316e2de4d6d5f309548d028b7c3363d");
            sb.Append("c65a4009a4fd5ea821210abe58f9cbffa49ff92632b6bba83ec76ed06a8ff03cf2d628090246a7cff95bcf3498be8169cb295d16e6ddc0b25a7c53ae5dbf9da1340baff881f95197c638bfc9465152dd33dfcd85780e79061a998b225ed07a87714ee3d56d83ee1b99e798a54dcbb8fbdb5228f32ca55c04b81a93cc55cc4a7124bf37969266d646dfa78df8beb46416243284d4b31f75436243eb9bbee48b5515d6bb30a8860eb728c6a814e0f7cb964ce382b41e34f239eef045e346d7e492d9faf06839ec8530ee566501ccb6bcfb16e104b0abcb818ccae321da91574d5d7a078852bed8d3223f79367b0f5a921d84cf2217f483456b594fa762849ee1da966310e13a873f67b953f52f4b6c895089200de1a0ce6a52278090ab7031c7caf814066e41e34eb5b73c7686ba51dc682981db0d182381a1b5a7126b3a1bc152c176c6875d97ba283973a9d9a99d920e9bee6914d52711c5a81a284ffcdc6c2cee8789af1afffd1bc53dc2af29537b324d6785ea7b6ee567d9f0de08abe4bdc4f51820f29f470c2a35488e74cd1c5f73c6ef8d79266c2c9655f0d60095d6ef1f91ab288a87147d20a42271fedb23ef4e8e1c14e2ebd7a2915aa9a891b983d6c7f6dd2b5f525f979467683aa5ebcd65b6771be4f63cc06c047604ec776453907c4568740042dba036d4b96e384099e27644718213");
            sb.Append("728cf6ded83bffae07b05d5627181221301dade01ca3d6ee6b445ca4552cce18491cfe92e857c80c4de2860e8ec5ac5a6305a31309670a888e7cfa2aef6172aec90f598c4b4d4d397eee375cb6a9b130c27df8b1c9c0fc0455c5fce1bb4732c2744170199a8479b3d4168160b335afd4e850a9b12b05d988bf7b7297b19165559b3f2cfad2c7496279ffa4e652ee6592618e5d7ac9fdf859e193b6f8962c32059f7dfef6fcdee43be5d8ab254173fb27dbc6afd774c78c909a02227ddbaba8582370af6d27d50f91639c35ea3eea917f91467b2d39b1745302545670a26a286b7871c1d51b9c2aa712b9641dd4931f186440fc2509a09550a0e8c33ea927c71db0c233a8d595ddf70ed5d09efeb8a6ab36826a38ed14573513546050dad774a5a342b4e639b20fe64123804806d97a6c5de616c93c5cb71e17dcf730efa5568d49ae078d3981843f6db128f0a8c0c147ec45b43abd2696582a300b347664387cfec620da1a280d262e7431944d3f1b09cd5b125c73d17bfd4e4a30386a1910aa769bad27ad23ee70c62391dd3ad56ea10a9a0be3cf37c65d78f5f5c44af9e833076162304f26d6d7c827e75754d37b489d1194ef8cc0a7dfa4292eda9b637beeccbfd87fbee3b41e5c85c554000f8a83eb47a49ae67ff0d37ee991c84f91054b099f49e3143fcb88794532b3451c23fe2a591081");
            sb.Append("16c74f0eb8b0219b710de5f2fda046295a42d9eaa4d0891ebf888a4c82f0f97f432fc40bd05dcf492606b1ea450268bf527f0b31201e43707606dec0062a4c65a6d16546b4684cff72ad3edfa70bafc04f2e14b138c3f2a61ec1aaf92060fd3e93ce2a751090c7a6754d254bd337c5907bb7c6c99385db365a9a3033d0cc193af5cc88223145669a9c6b7550890ae07a7b55c4ae2f09510c24448a4f27e5edb7925d1b9f196d4fae5813e5a5098de1a29181f52d56073bc706ca14a64af0d92f40227a3c2e951573f0a7b1dfba1dca8abb8f0e55d261b89a30a72186f43bf88cac6de66dea97d7373f2375b456ce4459bbb16614deabb6e69f8aa51ce1a572a1e80147b1c9f4c0695e6d73bb3b07fe1258f087aa52d7f6b3b5902d4af7cbfd88bea4e76e0a936667b433be617cdac5a37d11c0f7fe68ec79b6faacafda34b3e07ed6d609e0b7ce2aea6782b396e378d837af87fa125c71ec8b532f61e37d7c949c33db361dcd9a98597321c22e93a109cd76a77fd29856fffc683b489161d9d73cd283c49da12be4c149f684aa02b6d681dab301f5e29a5a161cb66a6d41a572391288bb0a3877e729dce41684c77931016170d901cb79728fec6d6ad6fc5b839f896ad0a034f6d8280ea1d5c5e7ef53d4f96cd06076f20e516f44be8cff427798ca7bf96d3868d0055b8b7564d9cbfa8739d95b");
            sb.Append("a9436b1c33f14604d96464b114962c99938553b7fbc06fb023a61613f49998e4e9fd19300677e5987ab8de09f0bbe78289b7d4be2103135fdfb6f759500c3143e4c3b4907755f7f2c6468d206019b45ab73b2baaddf7576875bb2d36c2fde82185e1555db05c89b21a3350405447cb689752f051e9cbaa836a8f0a8c473d1f647ad847f59745aeb408b6eb176d4b947d06c2b04650bbe2d07898f5d83e429829f1b52f82122a8f18de068a2a06f06117843049746e356cbdd6ae8c8080c87e41d0baf26a66a7f59084add25b815b76c6726104ebffdf900245fa6dc8471d7ea8365ddc931100998cd1aa6677230e7dbb58c9ff629e518610f91ca54563bc79882c665cf069f541bac91d220aee9ca62b055968c795aeb0f5d2693a5bbce03e336f73a9255dd862336f6ae14ee24dd6f03144a2039590b7f6131b7a0977a900bd37d3f253cb454128da81ec8c03cbf650baadfa4420029409517e374231ce4e4ebf1420b34353ebc062839447ba02de6aac66694681723fa521f6c0ed9fa8973e5c996453b352964986925f6cbdca5d7dc20e3e64915ce047729fc0b624b6097722e446d4b847d72289b8e132e6d7e7f9456cbc51d1fe61c0a7a052c8e04f24782cb0dec21b1bc365ad2f22f876e2c1d4eabda2b8ec6e6e9cff6df53e22fe6daf1dfbdb3bb73c32dfddcce9602e9e4a9494b5375e");
            sb.Append("f2092a6dfc88ee909891c368f8a9fd3df9a4dda891ab176a9fd2c5065bd1a1a3e093179a0bd7125e6881f9b855d2252cf456bd4548eb593313fbad5685a3e24ba81bdf349b372b46a99b43fb978f58b4cb350e7e9f5f5a1aaa52943de952d8480211987bc554be107bac01c02c33de93663dde2ba81399202e2cdda674e7e8ae7283d82df7818143e489ff9a26514ac17ed2e9f0e67cfff2797a12e0d70869569bb8cf1dff19e198258aee4ab101a40e7ca84d760826f69ab3352b75d8ffb90a876febd2bc2367c18f75d8ea1c165cf9a10e7ca7ca6da833f09d3edd22611e5bbd0d6adf2368fe9c8cc07286e5cc1cba771485848971fd73cc545ba0ef3cb42d5c45a20b6745233e883a4f0d35f6ab8d4480a3d1f0b3c5c60fba35a782f4e73bde5eea7c6c122f1abf9ce1553535323fab3c8300378a08cdbe18b1dd997e15bd38986a07fd65f5fe146404a56c32f912a4ab91533c9cf0d06844cac98064a8ac9c49356452c106a0873613f049314243bbbccb1b19fbd7c483c31b65b13d38443caf4079bcdea8bd96906203de6494446f0e2dceb08be67549bae844b6d663211cc2fec8f4f0e777482fd6938a699f5c5737fe08b3ac00ab4a52959c8ee65b3eac65eead7ec3a402f1025aea6125d37d96ccf6ae9e46c7f7370d2e387fe3f8b1d3965c26209f391f6017191a61525f86374afae2");
            sb.Append("3729e53b5dbe3d6f7b698ca55a2312931189ca78d8e45cf6f6296a21a8882e81e39798cf039d9dae8a11a5189a3f249526aacb406758d550beedf4d0a80997f297be36bbc4a9d4441d455d4d7f9679d2f787bd0f68c1ef0f665a66148b4ae55b3b3671c5fc44bffd4da427dc82565fc621fdb5509e27190afe87160ad647ca9b471b1d85465d2f919f0d01ef76bb317745c3d6441f5d27c6512da7cfe19573ea479598816e28c83aae39b42426e888674d081a7dd3c98842b405d09b0eb7f06cced9429c630834683a8d600111f7d6f55aadda6038b7bd9ee14c8114c32aa4ba91fb9fab2796acce65f938ba93cb377a814a0b64bc4b93b4a1aa6adc8d70b0d9b0a5e17825815264b4b267065b478adf39e429e8b29a51200558a4cfc49a4559be65aad4584148bbbff98e1e1c31610f445bbef9f923507374a9f1971b9938e794d1eac7182f86516fa154339cdd429956de220843b52577c54bf8a4e5c0ab94010eb72b8573a0f6718c502ffeed56637569a3919ba3783c9cad5f1d7699c98d2ef6fb7330121cb5c4d54cf548ac2bb9c346607ed1fc5f0930bd2c8de9f40d0d4198b86f8b7a8b4b13c14fb645bc3a9eaa1232aae83530f420a9365cce4d91d854940107d7fb2d2a900ca7cf6c070d725d7467be0d5b2ab7839e0a02eb37349364a1e913a2f528b6bc6457608ac3fdbfda5d9604");
            sb.Append("cb2e4a917c31efb56be53522383de8f3e5ae58f868afff4039280198bad195b5f802b14afbbbcf0977ff582be8dcbe7109ac4f4653d67ab4397f288b7dba430a200fe3eb3ad90fb801174e4bba7abc09748d05ee13a246725a3eb7668a917bb582162c969f577d906f2937b66dbdbb7de375b850cde35032bcbed9eef89cf8e3c6e6c27e31e1aacf3cc1252614e9239d5b873fbb9d02a070b2b6e4bdba319e074fffe8c76e654b862e6e7f028b6ede63e070f4cdf180fbef466348f2f0e65ce80462b6ee23c7d6535248b381d719e46b29fd55575ba04d5369444e3390794d09809a766fac740c1468cbb01ee459dc2b906b2f82a1eccd08d191f0e5da915a9c2edda1799b5264a1f45e46f75a84d99e3a0408d5cfaedfb26791d29c00daafcd11a181c697ce58cd612df17888e4200fab5fb0d2d48702d75060b143043613f707d450d3da37469f7bf3df34990c97609cf646531d92ef3b0add9351e64fdee7c218f9212e75c4563b8de1c7246b94997c41c3e071c47f356c599f2086f80442469561c71e7b27fa338d882b0ee6ffad76d51b03beeb060458bfd839a6f8ec43af4a91740a93a52eecb73ada1c98adc2ab4950a3b1abab50fc79d28eef6b4465bd2c8b912db7ace5ebbb409240ce47d34b37b62ba6286007cafe89bd1eef2159605780469a34c7edfdb2797e1ef52d81e022b124");
            sb.Append("8f35497d3a7406f343d9eb12c32b97726e20742f4ddeef5b3efcab7349c4b8e6cea87854d8aa990578f791b3b72635baf36ddc478ce2ac81720ab9737dc9970e7c69b996ce55b4825e0ceb6ea7a21f1b99a309bec6e840238fcdf7268abc797273d3836b6f361976d75d365bad5b16868d218438a30fcb0fd65a1d4754d3dbdd9f2c0a2226dd85e928aa7c77a39374f34f96332ef0189fd198455c87c260a1025ed7676b58923fe9ef20684125b3cea971ce4eb37d31708807720472375d1f3c8f39ae9582cc726f0d5765cd28ed52f4302a2645c722fb21cae47248a958367cb2312538d82e788e01729512f4ef4de9254e3056ad8124b32fee7c572380d5fb933dbf8629c8251a5caa5ae513ccf7f73c85b577f56b50ab4e186bb9e10bea0bf05e6419d535d12368a0be686226de487b8a5cb57c21269ac23eab2bcb404bc48ca9287c62af01a8434351c212eb02762868fdf3b5bf19ceae4a1112939e6acfb5177f68f7db4e16a5480a7f3e784ce22cb439863e5f7e13e89c8db02c5d7d51fcc3724d4e5cdae9d644fdc0b47076e0db3b0a2e4770bfc49832fea6f1c93de7ae6054cf5d57197f4fe56e55cf461379dd6fb2af7811e2fd47f125c0cf6cfbf19db653fcf26abca71126defad8968dca75b07ffcf8735c81de04e9e77b7a052383a887a71c39d19caecc3a4ddf76fdad9c2bcfc6");
            sb.Append("5d4a328683319ccd30a44d82c57068b630d1d405d68170b3ed04ed6bd3a8c151d947f56f5a8409964c150004fc24a46323e4e8e183284a040b85fccb230fc29d1f7e504f4b7f9baad2aef19a0b017570f0db15804c169a79030ea36676a0ca5e8ab4f1d3e7ae0016b1263bfd5569deb5598d626719e72024a73d8c4fccfafa9847585c640acb1f274672913ed7c9548b0e43473d1e093ae0f4acfbbf0dde0e4edad468ec656ff27f644be3c13b4d08544ce3fa42795120c3ea58d3dd025a512e5245bb3762f77ef7dbfe23e279f7eb9e58ec4755be105595f090c42d1b552335feec94834e9f060189782b3cc251a560c0fb5f4fe5ae7693516da7cd0092babfbea3fa9553ea7f502d8689ced0a6c0e4f3bdbbd5b64188b40b6ef93cdbd9fd097c158e97cd67065e6a37f16a8bd362b4648e99b2909dc1d73707cc6925dd991e4fde6bd119e290f7b9032d4844785b3f6675da77f85aef0ec1e324fea0cf19f1b4cdb060b68d3f3826604fb74dc9b4a8b5a00a2e18dd3ae7b95a695e19f3de7efe9e4f19a5ed506a69187749b268dafac06f2a4cb39a94c4f4fc654a031b7f4774d0c0db5dce034616f7de6aa91a21843b2c802b7093af3b8c1f6793ead7a35e52b5a7c415ffa04767b8125706dfa1aea332c294307199a05fd9f0f51d184a7bf7c40a48b86a066415ecfa34df36491dc5a839d4");
            sb.Append("0563da512a277971253c5436c2c92daacad316ad56d2a36b0739a2603683f322922d51dd249b5e262fee994657bba55015566eb2f7703feb56f645bd295b9507d141c145f6835dcabbba00fe014fd58c1102d517385fcfbe1f9b8437fcab876415f386a443321a545c79c63f7d7fbcacb7d6450edbec92b036cd52168d4b8c27c0ab2ef658763108de62063f651c680ac35e0c8fb0062666ef52ca43be4de3bd47d81696b4e038aafa0d6a992fe700cfcb94bb60889d7183eafe4fcff1a36b19def4f291d03df9ff6a4360ac41b67bf14c15de35e0a569473b53b6543e83b7c028e8d794ca63ed7f4bb6cf427b48deac2f7db01c951aa75ae99a74d912f7b8299b90a45743374a337a7c394dc21280fc6fe385ed5983eeda36a5b550b51141806471befc8b850088d2751b27ac31f1e0ccbec853257b49fee9bcd760148c8dcef39d3ef2b87256736bc602dc424e23b78d48df015893513b9014c300ce006c2748e48ee5e8cc6fa28777e16b0fd4af0084f86c0e09efb0d7809cbd9cee59df82aa9f0a15668fac740625dcde15827f6606b0e18455411b71512c290795a3b0e2e571be7b80ac9b20e9afb9068d71f2c5a0584c1b41edd4d50312a54683c766cfbef15645d705d4fa7a782fd469374adf96f11f509b42071ad4f1c5f2588d96ad5962889676c1449ea6d4900335f6b6b09857d904");
            sb.Append("9b70997d178c07fce011cc1ee3ded6ff032b8560794742f48dc0b37e9b4a353f1560df17a90c9f551eec3e22a98322fe90e387fbb1e10fc359d0d8846ae3ca3081abf6abe9c2abfe8d17b93bc37d1530474fe5a3d0a376590cbe4ffaa762d98ffa73e5992a742a1074631506b541af51846641f2645ae026d315f1f8a3e14184d8e873e7c28024491d2bce1c45418bce51c230cb521d79cf76d87e5a8dd071a221565977f67824ead3088d16806b6687f40c1f722e1843c0c4af220f6902781d366873bdeb82848af83ae17b53fde4f703199e4b88b2164c8ff8b6c59341f07d4431652ea6b6cef014a6964fc8b87c7f2ab7cd84ad500d754976d82c4e16a3082442173ca8c58699a752f02c8b49877ccaaf588c231d771eb46de1f88f965fe53e622fc89e38ce0043bbae40f057716cbba0f227970ba457a24648194739547efb885e774c584b90ff179ae7054b0a3720dd48d04956ebf59e882b670a27debc6b5312d2e478264968bd43e499079be30b365fb38e1d42c87da223b7cf65c36b3bccefe1d0dda8b39afeed2b2b20b84d46462375082cf9e037f49401ceeaf7400f12e14478347864883b0818a7611af647987a0e8ef5283e33019aba68c2e08e65da1c980665354006470a463dfdf421dd5d8d09bb018204b9d240efcd9840b88df7bf2dda6d628ec7f2a10df00b2b152d7e39bf");
            sb.Append("43f7078bdfc872bd4c5a9b5af87510b2c431dc5eb9bbad18039c770342748f45595cf76ddedb8271b653a965e4b29f8e480b0114b0eebfb9fabb0f19068361fee57927090d1d9db6e2a30de06dfd9b81b80bd55fb3b95f5a8fed1ccb1ca0dae35a0c859d7059baf99285e3687ec2e6f7e00d2c33e2ad84754517425b861a30de35ffcad7dbef738c2595d05a0fbdc93cd446e7a20b52b36ac5cf543b6e71176678d87edc21a4cac2f7c1fe169a05948f18a3b4b48c5dd1d48b02afa798cc8b808d3cbe236a0d4a121d0af447c49c85f99befa1c2b69b6bbbffd5c496a1dad5f7101b5b59072dec0666317df85b108559ca35f224f7ab9c1cb38f473c656824e5a4111fb4b494a03373a4018aa639cd51c653d8f0ceabbf83572025ea3e9dcdc406ab75a2ab56498cf7d880c9483cc3a4fc8eb2ea3a1e62b2c4cf26d990994f6026af3f7fdc1d7dbc68a06d6275b6fe220dcb8f4b32f1ee9294e17d1db9673b7177c43eaea617ae83f3982a981c534ec7a0e5525687950eb6d838969447542fed29253c1aa84f8796f017203c0293abfb8f69e69c04986b0a2a2d15cb6799d5b950ad92ea4cf367f9d1e34cedbd45ec192c631cc34cea447275de68eab1ee9e5502f3438b1ea2489cd521a8f5122cea22029cce8d570137482122fcf8e94f032eae324e803dcfd0a0cb89da1558e8097aee853242");
            sb.Append("3a4018ae20137ebe6eefe1dd34d0f528beba3966e7aad164b5c382b41bf606a69162a2fb3043905cff3bf51f08fc13332ecdd45af9e257ff20bea3415b5df49b6cdffd59a65d03aeb2269e68d593c1ebae76d4e6dca5324eafd4f92d09c06d4843ff47b6f9c0a77c11057bb3e70966ae1976ac59d0f58e8ea7574190ec7d00c7036d375dd3013772782acc40ba20b69fe0ecc105bcace905f595aeb28ac76a2dfcfe555e02fa10a53d3309df36d6442ed7552031ce156bddcd6161b4a159292ab8e2510f8574f7cba4a4396de49813586d3a310ab3dc3d8cbee790cfa3edd42c3f91368804c6ff7c28310c144fe15a6cd46880d5a88f8068e6ef3add35883d24bf013f4215823e9c5193624e769d6ffa99cf653a7d6d199fa060939d7807034b489b5313386b398368fd6aa8423c0ca1f5a81538b2a91a9514e7e9e51a8338c1d34cb67d72c5f7afaafffb50b6e0cf928775e736a9a19b4b61d32f8237d91a945e1a114ecc3d9dd818d586455ee4a616abf374f01743f9e7e7d03b3bd3f62ec099ab54266ba6e0703ecb93a644278a6fed28ef0ee8af14ce58d675c8c741d623776b4a174cebd6188aea34cc7591e6846ef8558da73343a3777abb37313c599486f8a4fd49568778d286429fbf24ea0f70b1cc739b78410bc7b6a16d9fa0bbdac9bbd6673a53dfe954a7f6b935ec646163cf690c");
            sb.Append("4a5dd9737f804c20db34001d6e10203fadc2ea85da87426a3d43920a1eed5e13cf72c792fa5a644983a218db48bf4f4533be96025c33cdd1d49331735c3d9a1038580f6f874ea0bc63a63fc7be29ffc9a29ab89aac2f5746f7faa0190c624ada29b439c1c64acebe9cd4287b3ebdd39b529c7cbb62214301f27b3ba2ff07630b0cb1e0c5ccd4c07100404e54c34330b69f592d902699a53470a73f382acca1182467867e148d7be8d6e70192baa703a133efb5f9d956dd120f949c63a13606bc1fc45129129ff03d23d83b4c63c762e018014e7b10f81b535263882d9f28578b4367359caf4aa3ef477ebde82c1c3a23f50e5db3169cf959f5b8795b326a34cf0335ba6773ae8d59bf99aad21a79cad2bd637ac79e2929a5d546c8875e9a575c90a6b08b45ae7319e0da2be37c137d01d46732c514d9ccc264564c4ede6803600ab61d702fe29261dc564871d2bf7460c8326587e77b8e3d1600a501e41db419f8d5c42a99d9e2ea166082aabcb8acf3e73b16818704ac009dbc344ac3f891d4945e7bb021844143d7cb81aa8403527b19f00f310a7ef519e008e066685edf7c19024bd93aac982a8d0647ccc4d223de75a93d105544033ac52ede9804c6f8877cb5f7d4ce802fdc24bade089ac5601403e344d4409e8ea16d75ba5dca077d2de99eb7e4f62c866dc48bbfd8ffaabbabc4364b16");
            sb.Append("7444d908eeeb097eb6ccd773dd2752c0093c417c89f96b25581e4304649cba877283845df21b748532de30c1159f3ad2c455c1bce4ec5ad2c84d5556794c48b87f684af99a2a25fa133a18b04c50996664722cc581bfd9c1b6d9cae5f19b7edfa78d8e309446dfbc2bedb96519b278ee447be53f069f2e7189f200852cfbcf8ee44ef954cbb3c049f1ff302d60739d0dfeb04fbaedbe7a89ce8f7256066b2f304e2532e65b8fa0c59f783745194c8ac618279a49797c35448afbf5a1ee872ba90e078951c34948d8073285c1a1669fe1d4bac8b05294c3a5b996f3ca3433d4ba4a41f87d1ff46a936323df9c358cae1074baf3b06feed5865e8e2d3d20711795e4b6792daa75aa1a858d58d4e4d8a9d315ba485e02cf00210e9af5d326af94e67cc5da32b80af836e538266e4a64e7af10e970f7ece2590a0adbe9d1bda31857c548909b303756b24b4fe50255c49f8283ff3e18daf8f8a8c0e908a030b0a2ce2c42674579150fe2a282f8b77a10e08904921e2d957ae8ca4a76d497a014070b8d5e1a074cbca3022ba6d99e8ef0b841c8504fed244508a9c2584ffcd7f6332ae5602711f67dad085ef59669b257aab74bdff4685cb335d44c22030a81103e4ba8b9d40a4dfc015a804b9ac75e2023f9f43e6371ff12eb9e65582651c4c509810439f9b9319b182e1cbe98cafd08049978c41205");
            sb.Append("a777e9b1b92a4c356bc6b5a4cbccb149821159c9785f4df7193a6ce27502c992644ccb30ba8fa68fe97553fe9d4809349be54dbd95569c41384b220380c08be5a394e121b04063bbc25c078d8ec7eb24341871fe1be778a8f16067230d6e5949bf7c3fb44d41d276c62ac2ff1abf370d2176d7a3cdd4735f60db09ce9498f04f8b4c5c2e6d3f1bdd4e06bc2b92f7913033931dd41ee6bec9a6248c0622d50d802de1d826044e8289d6720a48b1aaa283427af158fcd72a57cf728857d7ec1208f70ef8f7e0cb331e30197b9bead1561ba1e3f7e5539ff0f2a540329001ce7418bb1c68a3d039c2eeb088a5d59d1cc1938c84264dbe09871424414e1871aad5251ba9476304020b5ac560c4ecf6cb1fae290c476375247fd66f79b440f634894cea77e33c831af09b3c95815d2229c2edb0149d801c750d7c50346c349c9f558f61195391c217358e7637afbb1c12230161d506fdfd443ee33790816eaf177cbc5a30d9f6f1a60bca0eefa75fef33a20c8b1ad73889df0ff61aa72f10e209b845b5de83c90f6a79c831f580df121ac9b256f2878a22cce7073785e0b67f337de9564b1c85f9b55aaf7dd8ad6192814501e2914a86826acb7b4f44d85a62a6d94bad089287994ec6db2a30cc69085f6bafd0f341a369caf75b38372e05ab7dd3fc681cc6397da3fb3232affc6aaff2f678f39420eb");
            sb.Append("ea1e0dee0020e206192c0d555e4d2bdc26ffce6391c5fd14b5b2157465a0c8f771bb6a6545569699a0c5e65a3373ab79c87501524fde072de28ad1122fc37636043184d0842ff41a077a72bd4f02dc3c9313ca37d43ba20bce37e9fb6f71a42512a42f49136f544510abf9b3da14ad092a7c8a06ae41f2f00449af2091e1ab9f95ee93f820e5c2a920695763425b1373ed37d15489ced4a2746b77bc1b052bc2e6955b3c4e17cc7553277fecf9d51987f2beb2d41c8dd6a751b796e9f8f6c8ebcaba9110f1165180361c28d96e739ed027c222aa172a46f638aabb10934a2604420a126a297615277c85eeb058c1811031ecb207b88251f48726c3d28b6b5e6cbd935fe28945bc7a78b25bf175eb060bb10b4cc09c5b36c44fa1dd2c54cfe7d59e69785c5eae14f988756358fdaa861384d4ff1184228e2b804304ac7f3f0d2c9033416e59355cdd24eb73374559bcbb5ca2b52c7e007cf11be25bc40851ddcd5dc1875286e9d46af8f98c867b855472d97df459655f7978667e4d1f6b55f4d5903e46970252e33d671fea3dd8fe46a1e4815d9011c6bd30c72947884e77a4dbc993cb0b37802f2e0777d0c1c3d42be0528934c2da8bc3418a480f9dba0b9103e828f379c9146a7b2df91e373f8dc00b9a05b131692885afaeb349e3ad3c5a1b94d0c1f0c9c2b1bea994eb0484dc3da62f20b4f3");
            sb.Append("3919e9ca1f883e26c0a8870409ddd929523d7c82bf830068aeab639532503badfe638ed59d6f073c955199d7c85dc4b5584dc70a2c6d86591e78e8fbdbc68aab26027d4d9e0a7dd0b33917036571300039bbd750e2e1e95214f3991fe5d0b4198fc8efe75f022bd5d292498f23fed7a5d1330d7d27a3701f011fd8bb5343208e6b7c9c8ea5d5756326643d88b0d3663fbe33eaf035256fd66090e31165f1d91e6472ae2ab96a06dd50053e38f60e85cdb240a6c6a6e2570738441336d84e6b7532f50bc6eecc0d4109fae4ae213a2f208a921d6d1ccf52bb38ef3c2180dacc017fbf45d8648fc0f7d87acc14202091a3c81f245264ce451f292adfbaf98df7d8f5a8f1d668a7351ce4f2a559a153bc7395da74cd8cd07a38ff932dbb1005b4582b95be4f225c1d06c8418d7e6c80ff21c29e0960176f48e55c5363cb543ebb1d20b3ae966fed0465a64adf66fde2f497baa381b53e5885e481dea23caa640c2ee686aabce14fc398909b8e8ef67c3be4b3c6000f11a0ff75b83ebea933597d3de95b4ad0cfa1d85fb665b554f17620fc1ba58ff3026654e75cef5cc964a50af5692f6677563cf00810be2fa9ec09f5cd23a2327050fea047de2d4b165336caeb38dda8cf2a57b692e7512dfc1ba5480cdfeba6dbf820c3581a5f127c74168954958995e43cbe91575a54485c4718eafdd8530486");
            sb.Append("fe81744ee39f7e06d293e90883efa1b231ae6912a62ba65afd4f8dfdef5cc06225f29be1489dc2d1707c3e0160fc36e4b9b952b8af173bf4b8f5f69ae3c85842fbee095c628561eb133b00f8534465ad46089982fb64771f1787d675c473e658edf1c53da39074bb1f40f4790a5d4e29d354dad3283596828e5755e8e6b9b29188723ad209f1e52860d259b1e784bc4745b5131f392faebccaca52d600b896fe210024acee04409cc93df5d3fab84ba52770f1155574d9ca20e34a17466e9b775f284d79cd886e73774309e2bb838d1749c3a9ea350538437a878b340c6fa516f29f49d2197e5a605fba584281aa9a3241ecdf3ed742d6f26381765e0480bd2b4797bf4917ebcde0719dd23ba92d8d9f30d4502984751303ae264511602fd6ec72232ee589030fdd24df0f17dd7e7b5f91e733348f869401fea0daea862834bd6539f849f81a2b928ac96d08a89c394522a2c58f19a9bf79346ab98abc8c4f3f0e2156b565203061625cdfe47fb6fd39547c4d22bca2b4ca412c73e2bed17fffa09bc908301b0278ddf641e5513de52014ec293ece31bf91f9f1d0c398227865087b1652d9069831d5f2fa58d2730a3f4acf1d773a078a47c3f69064fd3f9e29bec72e26fcc915db143b6a747f45de6037f8067b8782b6a27faa827e3444c16c2d8e5995eb2df49c649cf8d6a7159959695e8f48");
            sb.Append("1460626c6063208aa6d962f2463e3a82014148783a1cb52d949d46a4097396f09cbb1d1f46c7f06343481adcbc41b0a71f872c2db7cc4dfae27acf7543a8c5edde6d251d89d4cae2a65c2390ebe235cce2f88b27ff825a4a06e3289c983a2f125bbf785df6e7c60d224be60f72c0e773032a4bed0e90c1938786a679d3248543a680262eb7702f5c03832201b7dcb86ce84d237f920df6a83926de2f7d3687ab3b5918c093a4f762ebca07f918ef775555b12042da7974100e43ed10e8f38d161933e1473f7d44f5b50f5ff0d6c7630d4df29de7e68693ebb88d0b32191b7a4275e8a5f1cd383d11449c2b5a2945bd6ac8204e046794f29151b6e5a54e794bb995f7925dd2d09213ee03d703c29070d5a46709dbead977d067507db7da995da6e0bc472a7d807312b92cc22b70c007e55d519ebcbe9de1bffc96a6754ca1aa6d6cf404f55baba1a3e0962b89d3b763616fa42baff68d69d2be46e34c8304717f0496fe432b5f5eca35b238f98aa863d78168f39fae606751d75dbaed60a28794c4ecfe199167de73b9cfa5a20c654d6b1ff4651b1e0db9a9c78ad8e71c4e3acbc9d1e6ad713620da051735b3d955fe3abc10278a1d54db361970dc0e29a910256bc5e730ebd06aef68e79bb3247e1f784e3439397df2656c9d69e4fd66ab25180d269b357b434692ce2976cac5c0f8f77902b805");
            sb.Append("092a671b1db416449d77e9b914df8c6333239135ef62431958bc02e6ce50336ad0fb3830ea7c5b2e798cf0e594da03441f9d6719b1a0476f1594daad8ca807abc529ae89dc7be48aed33e9bdd0eb7ef35ee496a6fc871155fa4ab1298fddf2983f99eb6e2b94b8e8744e4ddc61e6dceb26ec466327a7cd2f8d892b039a2b6f4761503c742e9ca31c102242260f7f1513618a39988bdd768c5cb68829db47315aecf833897581dea13528fa788d4f5d9e45f1a3a15c6c30450302ea861b90b394d785252e09d59e5e373d9fa2fd2e6ec1a21bcf38e268d88751cbbd1905a73f97947a7a1e511c72b3613db61f079d75b29f3c4919fd98836fee851c4a23b422935d305bce64997a29b65f73d3edc559d32f2ca428b401cbdb8ececbd7fb33172c01a116a83c16e860c3ae998a45570e764e34f620ab8364e75dcec262bbd9c966d3509db8a4fcee5cdfca4afdbf0ed32d69b2bcffbd35fb06971c2998221cb0e6a75c633d5d042e6f80389612bc7d673ea3647940fae881ee08825c1cbe7cad2fc9462ac35dea2d428fab596d62e0ac172fffde5a8e941e971ba2cb9c8780cf26891cc694255e33fdc79a49d32e2521872309c7e00338e1b4bc42e9979a9d04f02698face164ab81014c358538bf8f13e506f49af2be4ece6533cf24e6b240d466efe8fee7f11fa1f79f80c6a96675f9f95bd4fcb");
            sb.Append("68197598e245c32dddd3a4277c933cb868131e201242da58b6588b4d3740dd73c0a3f11a015dfb6139341e1fcbde741fede2d334b05d08c537f7aa309e6af01f3a45f8ef26afe6e02e19ea06a6082f288b028414dd284a6dee7a36df4d3a11d4985d0fca55fa47a9b7aef8f056ae121ea3fdbadeab38bf098b7ba6a4d319c63c6a0df94a70ae3e5a5f6a500fde3ef89a62f7acf1c392d81c300335e82febe86608a8ee9dbc22955484386c6423410ffed922306cde097a50b636c3535b379122814c946201842e6e5c328c47e56320b3a4f74b666f2b3b41166e2a1ad5e2ed3a1d5e08b48a0ad29a558a12cad2759e5bffb90fd519002a8d025e3bf0dfbe6353eb76405e1da548cfdf6576967f2c3fd68d824e5617ca92e0c49e575f8b5262c823aedcf8bba5cfbfa163fe5ed3400eeab61ce7e92c66939e271a5d89b76c267433da37443d6c7ed809bf3adc232ad248a9fe8e3a8627ee8f038076738bda774bffbd83c78f68ebe052845029a9dc249d29503ddad974bf647ebc54a236ef7c2887b6f99cb2b937d1db27d8e13645ad0692742ac8c74133b78700c381ca31512621b72e2856a76cf4457b104a4e98c8fa70b70a54a512ebc3e0974e8618cb7dfd3c20cba034bfd71b45870b4fd866b2bf0a37c5a33a2eee8bc483fe0bbda9176e000ec2170fc837c53473b178fc3643110035866c");
            sb.Append("248b47f659a97b31274e2f264b304db2a4eb7ce721f61af6def733de6b02482cfe7ff2260727a4989685adaaa828dc393c432bd0b196b472ff2dc33bb27f8a7413cbc7673c1067ccccf4c3b0c504e8c3905e0dad1126f31deb5c1b29e9933d21f1766b8cae36fe29146ab4c4efcb4b742461c54ef74698a9d476a8c1e591cfac0ab8de356b1f1f6ffbf31049804a6922238d89e3faba4736baeb7744c53bf6aa7852066956e6bdfc9f06bd7b0e5f1a372ef4c1a3e13b9c2599712847e78181aebe779c3cee895464d044133d8a0fb14fd7c098cd5ab04a84d474e07c0f46bb5be99cf87a638b9bc8ed118d68000fada1d02aa45cf85590f5398cee89c7a4722b7f0628d7dd41f8075dbe56bc5b8036bdbe621fed2ed2169584bb098377e489892baa5beebbc342bbb474f2f9d1030715d2310b4e84886bb8618aafc1aedec2fd87c03ea47b192fe56c1db360fe07e8a7139400e3eeecd9a330a038c9f5698e6801159eb2ce97716d7c002c3ee9ef7e46cee86144da20d2c9c950694c18b9840c3445b30f45ad2f2bf8f48804b559c1e3674e6d7744e1d5c2fbe805cbbb07d713f2a29b5a9eec1b735af8365381290e5d0836d4783ae519e25b2e6396ede19cec0645db68509deffec254068ddb71bf62eee194a7ee39c4ff384434beeb550f719ecbd29a216eeefa97ede634f5de6681fef0f4b4");
            sb.Append("3764d27d245e3506a229c36e848bba8572ecf0d1977a9e63ade927706ed5e082b04ca84e35391fb6fc5dc7ee90f8b978393be679cd3a722d9d6da07241feda24a0a4b5da217ea09e5ed8c6715a111937a58d63ed578cd38f648965eb276eca67eb4308a53253684b0374463453ead6bfe088cfcbb28f0554acbbae63089b3a152893a9ef77d0170cfa4ee95b67d1ca313e0464157b5f5c5e1d23e9b1f6f38ec4bf033fffbbe243c53a64ed797b5c69232119b4f97349e819c9d22e936eda19cc130b78d018a2912fe2c5993704c400f340c3af7bffd1b87aefe8603facc76c304179888457e3b15b9517539833754a18a46e6948d40651193abf1edac4461d37aa6bd8548c7af833b921b7d2fbf1806594c1288f3819840c3f35d9065b08ec3497ebb4a24a9df31825aa8d6bd8456b6997018119fc8154b0ccca0d85e480a0a2cb797356bc96256c2c73985ca746ecb02d971664b8e847c0902bc59044245043fb8fab66c24ee1b64d959dea24cb67ea7e05fb347ebd067291416c0075741b68a94d795dd81bae344651f49ead995176bb974ffb27a2f2fc29fd18e070a956fecc32840d49cebc6f95198da51e18658c3c5eaf22b82f15d5ece49340fec23f65163810737df2cd3d30c9b8cee383ef088ea44a944e8fd9b8f9270c46e9669530e20ed4304093a2c532a8ba3462286f874de13491");
            sb.Append("2340bf25292bdd788cac30683dc2f4777a49f30785eb13c2c392172a3ec5aed5e4f6d2d165eacf4a3ef0b671b029001422de8d00b546bc2603a95ae5a0f5629c2b56a1797c520aacede11d83ff4b494f8ece6d894d8ba07aaa951a520ac47d07cb45bd14764b4de5e50d009299509ce62c1e91fac2e84c1b200cf856121f93fe9f287ef6aef1fd7d54f4c302ad1a715b8f1e9be6134f1ff3061ba3e93b35acf1f0211647495a80596afe16210135a9b64b0e19fe238ded5479741a960f37eeaacbdb7c017c89ceab89ed9dc61bc4c055e11336f8534497ca59a942e97cebb34b96063f3c04a798845c40319abc2939dc5664e4c07b2cc2800f681d1774605b8395362ca785e418217b8ea335da26b25a9893a64ed2fb5d6467bfbc4a02ffbec791ac9aa1ceb0833171840b3620b5f32f5dffd20b76f07b7d87afb1d95687ccca19aaa74a486e6a6d39e7b08b6448eb6e97c0d42e039c524414dbcf81a9dd29e6c4d34719fd5070c646f7618aeb43d6ddb6408f51488e625540936c1b8dfebc481f8196338fd36a33eadd33f8d8563246ad1db33b804e015fb7ca805ae7ad63a8268e49fc842259e8a0d4b7c55ab2bf7357d5e4794e89adb245046e5db3be74e43ae0872c950085d0c75d831c772265acb5e951b237c07d477f758ffc8b8c43c5acada86cb3f8a9a58eaf2055fa48b5a16b873b5d");
            sb.Append("3717684f6d1c9b76849f5a2ce52226f73804fdbf1144c1b54b2cc8a2815a6d61c046427b1b0faa5c612afe9d49734ea212108aa765e80429c1984a768bebdaafe773da0b788b2fde17aad6b1732709452bac2755d701fbf8bfa6fb027360d86ea043d049e905b05208067f0acd843f0a1159034271576a3cd2fe4c139b3d0406ca25d71d24aa637a0c6cd02ee192d5521c0f67e2abb4a23c41c3f959b538aadcc059812a310a1e54a873c9af8137de88ce34b2ddd89fa65f0d84e9586c68ca4eccadb27ecd95f56ab29a89d2af2a90c4cb88ca66dc654531dcbd4ae75c6e6c524f0db4129139f91e21624f40a3036d1e9989065cc0ca398f6233ce1ff9f7575ff7da7b75c98038a405433b754232446ddde7c05b9d30c3ce4d1165e3cadd750d7a3515cedfa2ef75c9e4775173fc01b1d1843905472214ce950f07d139579a80c52b9972ca13445b2042dc54bf1a710c39ee72b3602d896e7b26ba8fdef051553dd34ef6d990f61728cf9e3a544b51a3f2b65db9d7568b42ab08ee22cc7c2913e03e58286f93a05736e81fe10a2acac2ed29ce00901cea55c8b760e407a97fc240e7694f35a927f58ca5a2d22a956e86205b1784389fd3f88b92f93df3b13eaa6ee568019e3e843da9b2ce5c0d8f47b3432ec7ad433dabc4758be4c3d336b01f99439a493d6043167a72f33267badf750f4bf369");
            sb.Append("5d17d6c1203e278b8ea545f88b12a897dbb3e77b5ead80146a0c032083deef014c906e70043839b7baf6f6e310adc0618fafc27fead34ec765eb8149f3a33837dc8b827c4c03fa7833700bf9fc01c5a296f66859ca833908cbb3498f214252dcc16de257a99992f4424a83c4dff15a6a46849498532f6e46f5932899fd062f8827727a1ee0910725c925ec80a1d91b1b5c9ba4495f6dcad6a15d860b3fe32e986658e208b6504156fe63aa8579bdf1802b9989a0d596f7230e486102341956df404a268512f8676e71c530d8a0c2da1d7ab43e8acd859934095931125c820f9aabb39d1eee264c9fb3f1a55d80898bd4ad4c3c2f218c574815d0b88563fe3191d9c7f38bad28b50a41c48ee8fb4d276e0b2862acade1d818df8e88665ea722fa455e013247aa0329e8b8108cd7e4af520c38080ae8ac600f3a44fe44db1e90bc5892076f15d2a385e70469d96d3df5f1a430564dc63f08becbe565d13d227c8da5d1e90b4ab8852588d19c6c9dbcb5b28d54581ed336b1800b6c582a11bf8164c2076b09e564333cc76190662e36fd6f6d64ad5768c8784ba22f04ea57eceadadb89d784dd4e5ef0d298dda682a34594c57ba3a3e3d3d30a2cf96df7fd93aa825e73f532072870f0fcbd7904325a1e48ebf2ce04b3a30e4ca6416624de6cb2d6fdab5f6f75b64c815584df10b920e44c3840bfb4");
            sb.Append("76f0af637b0b15860dbe3216c08afbd8fc67d8de6d3905d4f67176514cc9426ba1ac069c1f514412e2e2c03922748512fabb90e72b4f7ba2a087ccf7016d56a14818f014c88b164a0f86b616bc5483f7ca017621e1a12663d26317a96a5f9541fee64b5794458849454fe86b081bdf098c5893ceeb7e76fb9bf5fc5d83bf61f95bd3c7cb7b1308f9cff586092ec56f7d1b33fd54b48ba41c90987dc04cbe84f24455308dae580a7d4c3ed35c8f1580bb0ca60b343f67d36a8eee7d0cd64814cf6b9fa28dcfcc2b3a0e4abaeaa918309bce61ee8dfe7d33cc6a0a20a580a3bd265871eb69bc63e818c159feb81b85308ed99f48079a6adb44484460f017562682cd7c0a7e0d2b8bc4e4d6f93fd51a67e03ac1f5efe8ed12ec98018d4a9c5cac83e95419e1eeca9b4c12991e80996363ef2853c1b8fa6aa97b396969733d3d34a2976121b2191daba01779e151c6bb40932d520198d8d677ee468ed40eba485438020599c8c27348304c86aa4c47f44d971b285a1b2776203ddd1c0ad6eec5b73a30cd415b924b797fa152f26fa77e57d49aed3ad9806d02c77b28119acdc188f0fc518e593697242d11d23bb2a823bf8bf74e9c292a9ab689b4402d43053883cfe7e011288e9da59bb17778ebce7bcfe150dc17fc82071facf68ff539ac91f29824e195c6bf76844103e0d1d50c3244a91d2d8b97");
            sb.Append("204438cb7575f8ffdaff80837d77a5d144cba603c6061be9649da0f5a217fd82dbf9d88f7eb08a5c9d525894986abc9322dbc9ed472d53e9c7b34b240d5105a5719f6c2f392bbabceefb3ec8f69b2e572c01bdff29a609e6aa550b6b7c7ed1569fc4e14c3eec6ff1bbe98cbf7f7523a117605d937fbf78bced1edc8b599106d8e0279a902b59039b7ddb72e62eaee6bd847419db0235169ab40e9f3d5b8979d4053e478ee55c6b118e68ce4b4e598b4b5a66b9a8a389a7baf1408ddfbacc4854c5ad0c1ce1ead10cf09d7cc17b5bfb613643ee8b90440ef21a33d9c38cf01d36bc2665c0757f0ec84ad7f17e4d81dce68d160df65b5604a84303163ec1e1acd768cc9a7028cec382e8fcbb486ace9189530d5ea030e50c9c706f357aa460e209f8efe1bb12159dbbb448942b8cf0a5fb52d091dbe646f1ea7ca9ce33cea00e1294934ec174d9919448061b7d2377038b7193a48f809448f341b7a584444c80e20cde7ebc575a6cf328461fe190f732e98e0a19c5349ad6f1f1698d08803a7f0d7a862f4358534134f95332367231e3ae5ad3d328c4b5cebd215c65b10bdeb702a1b52970c7c117ad39022ddc75d201f412609c1d65596f2df5e84d85386a1bc3e61ec78ed5a48c40980f6c010cf1e0a21eaad81e2b602833f6c498473769d6525f7251fc35dae1b1db00b266f53adda0f76fa6ac");
            sb.Append("0fb3d9dbe4ca124500dd0ee425969577bc7e554488db9beeadf4567b9409a868ff593d921c4043b10b02f7ac823876ab91a2de614cf918a145ebe856845bf35ce60a26bf869d8bd8bac370fe9507a8af50a6216b65a6af09c1d6d2b5629adf939936e6713e18d0494b571e24cb4b912e7ff8a763c7a8fcbad9338602085d5fdf47108ac822426514f3ba57f48ea120e9c518d3dc13048e6d09d08d00c03a352e2eb5b3665887ebb624c7aef159ddca3f9c277ba3968fd06da0234b7f49cf45421a0ff4083acd55c2ea3868abb40b2d9906889937d4d9c16090fdeac8953ffa5f602f31c6329555667bfd6ca0ce5c4d71ddc4ba9a426f7a97e70fbdf3e806d36946831c402a4bc8880d5ccf6bc12519341f6865b9821e461bd4cddac32fab0e2c27092283f4f228467784c6cfddb7a52f6bba39a12c94882375082ffbce295c218e0347d774027d94cdeba0abccb5af7e72e4292551b01cb7d29e230d9e52c03dada5183be0f726a35f904aac1e84d54a37b371c1603ee282a1ad25ca3d75467e7ff09dc65a6c408db5497d83e8fe0e9c8098d4a203e711e1530c018fe52532a5dfb59c71803f06841f0e0807954c42fc0bbe9c1d6709a3102841e73698785aba8cbbbfe764c8b700698fbfd73de86e2099d9311305a5fe566bb582281f7020772ebb2e39b9979e75173aea2197a6d78a548ec133");
            sb.Append("0f4a86c9d998d82691bacedaaace9c4232f540aa17b5443aa279030d5d0c8f95e851db65d9210974a3d504dca93129a0e927703977b5ef5fbc4b6c1d86eebc894aec561a0aefefc0ac073222a23d9ed0ba6b481663f46aef18dc95bfd1943c018aae254757a4df01884269e9e36ba64a6b9d03016d22d13fb7f7bf14f1d0c0fe6180077ad9a82c7af0fe6574ed5e349a31750be6b96192483b08dbf2b839edb4ac31ac6e54e55a95e7fc50441179f72c3464ced9644b4a291acc2aac55e192c5b287a6392473fcece2f6a762586ab4d8e1562845ffce1b2236bb01215f1703e4e80c07cb0be5a279544885f069e3bfbb69f5bc24aec7471b42b9d2962c4762d4bedabfbe1740b8a49900d9a727b3113db3609108bc7df8cded909c5fcae74195235ee33fb1085a31286de7dda9e2de7f9c9ccf0cc1c5b0a2c975607321f91dcff9b15314ad621b206a419f6e0e68985182f88b7148c83863af616d086d3915fd976bd3fb8ac567e8d3b87f9559ad152f32853534478d1882e29479583b3a4b1f1e8f18375806fb9df71219cb94973b6ee37094cffd34563a0dd67428265719c986a9de52c4b2912d0c6bff43bdc16419bb000119179f466d045234f7fa615921c3d2372a5ca7b6a22173346cd857a92eba428a601fef1741a0a47a931dcbd7c985f55551bc52f96fd21cb7e6330985d6d1c4e812");
            sb.Append("8c240f612dba28b17a550ff7a5503785356f309f535e1d66af16bd58f13093572a053bd1df1f2251d79168c7da5b8e847b5da6be545d62780fd1db1729a5627957ed7b9188bc3c995f94946e5b984bef08743ce42686a7e845e48e7104dd4dc527ec0dfb194842852b80eb02108469f1004f429751351eabef6eb96a5455135a0dc9130f12248e9512c47cc6248f691b39f30497b4320e97f9272ecaf743b42f2c8297db22e44ebc84d198743c562fc05e75e00c375012361e71fc96375b47e08d2a8f0e193c759297506fc30e5e9fa3b0deeb01edaac6d2a0b7c037509191f552f9be1ad48fe051b20d708cf64fe74ec03723e7c845a2e32e154f400037cc9de529a2af30a321f2b40a5c6fbabbd4f0505c4bdceb0787a140ea787a5463ebaa2b24e48b1110a53faa1312f2cce61cc8e5494c44d3c773e924f93db8a4fac9fb70caf8e3823c3d3247157d0bae472a6a8424a9ad5c72a110a5bc83f314e535c03785c8a3591f98d9c24e1e4d03a0d7d6fe8cdb716b54dc0b230e0a52fb045487e3532336682c2d4a87ddd98303283d4ad0c16e1234b4445b1805c786cc9986599e2e28553f5911ee65e99bc4c4d03f886ef10cb0b06efd1cf4de98a2826e2ce4e190640cf514d8d844301c5315063de11ddd9bcbece607b97c7f0c273ef9f76edde68cbc7e3647770a676ce32127bdc2edbba916");
            sb.Append("766f3295872bcdca5370aa8c13663b0caa8fe7800db18f75c3e90e42ea7b2971c918fd089b4be00ac627a016a1443c993a7b3840e18de7e51b25cce4ae843af5af700953da14f3a2f020f9a91dccca8c6cedc5c7215c99ac0100f99120f46fd795881b099c451af80379043ac8460b32af055b19d7f983d73f65bfcc03557ff26b70365e9cd316ab80e41b0c4eecaff315120ffeeef22b4c76bcf92ebba505ce39761b1bda041e85c67f71260ccc66ac46899c3cac1a182d598054c49e5db94a6045562769238be17b704bf7b084894fd9ca3e2e0db5dacd4e8d0c392b81f914bae93c82ae56d1f1bbbb9aa315bb7ff448e1e222781963d0515a6ed30d657fa80e5875721f0be6d2db0c6cf0d2a27af5e50983a244dd8307f8fdd82e33ba3bc46fcc4354b7897ed208a8a503423b6774371c9b8103d7e244e16348b233ca105f673ae7ac892d3b9e87869b05478de92b952525379fe9809853850e18262da003d8e2908e6c980ed85ecdc0eb621aef5731b8503bce760af5770c95a6f8f6b0f4d97f7360f8885a2814d7192edbe2c8c5b9b12faf33929a98c7e8cac3876b77d5db1373a42998d425c22a209c306c182806f8be5d0373b445755b381149462aae399e2aa8b767e940173d53510e8f3831935d50d8b1cde23f047f5b0682d9657aae403e84aed7b7e32b69acd2c1c952659951bf3d");
            sb.Append("0313189f48ddfdeda2d48e3e100cb2e6b42e6de39235c2d79ef5ff22808aa3f6cb1b3872b2972109bdae1013026d9b36821965c824769f14214dbe912c6add90cb3db4a24e94a03df8daac1f30f1566e85225f4e32eed4afe7414d85bab5ecd7ff422296e26897867220e7dd43090f6ec1fb931dcd9814042f56aa6d21a1c4b263db763d8ddee947660c1f91daa411199fc063b0cec18e7ce0718696e0eebb3a15f8c3f7a014474e043658edbb9ba1983ca84bc53e8920cd6e34f93505f141a6049b095bbfc24785a5eef326a38da3260d90c0b05abad2f185570aa4d3f4aa3b70326c6aa7073a078b33c34e58d397b4e5565a2efdc7680a1fd3a95eb6e2dab44c3571077faefd4c8a0e8d2b3eb1382616a21e8f5e15ef6d68589493d2477b47b75ce6c4485c3ff152a54d1c1add5793477bfd2bee0ddabf1dcf10af7511357f6ffa65c08dbc3c7ff382df99957d0250cd33f3ea35baf5754729f082df9255434082e6ac6d5501694f585ebef35959de2552ad640fc0d6ac5a68761cb865adf0bbe1d02c7733dd96d0557a9d026260eeef2a593d81cf01e022bc1c21fa012a9c9b8836b3243f555ddb8a06effd8ef6416a7c76bf2f5dab3ce76fd21e14ed96d01ad999cebedd0b330f9b85c512cfa589bf6286eea13a0bf007e2e989aed43d75a4206cd2248752a70b760a7b9eab848ede3ed262");
            sb.Append("4e5f3f8b7c9973260732f51bf6eab9884c148b015344f078888cfcaa3e0aba5bc213006aa04a1970e86102cb85d624bb1102392d4cc00b613722fde45c5af43e87742e07d8fbef814a2b1459635c51e526a4e10e7830952c26855afaa5e92811933ac3735ae49640cf7cf51465c51c43924d059abb2d6e2da4b22bea5751eac1d959cd7f31c02fbb428cc03999907cc7f7db173a827c8ac440d3cb053bcc3d635a373ebdec4b4031c55d42190357e100e8ca7c5b700d95c559c14e05c54f2973fe393a9359d4aeba88df2b7380a4c1c6b93b34678510f5cc7134b826567a1ff9222cba79c6eac94219097a7a7bfb9aa82b3ea46b53f0493a4b26b402c7695f1310b40ee96ced2affbb7b6b3cdae53c3ec1b021c231122cbbdba78863ea699e7f9db0e6e58d845ad47063f018fa14c5b2c8f24cecd24e6d0700a2449cc0846ac0e087bf8b2a29a168e5d09f2c02af7e15c4e3836a47b165689e550638c117ca8d2592d75603c23e9d0f1b588d7cb3a9a17d0b6bc46295082a9c680c4c7dc6e8eba48efb386314cc350d7c77411de80274a983c92d9b96ae57f5c2ece777077a7e778ab16ff1ea91494cdde0f74feef4b52d87b24a8c184c5234fde16a62d509b06e2679f03dccb839b08032359e1f7b5c523674cd1b9d52974ef3acfcbbe48ffe88fdd8b0cd341c449442fb7fcd8f2185ed228b66");
            sb.Append("1855a54055bb97e9356f6d3fa327800c87a7f7e576e8d76df302ac5e1e12655556c6261843fcbfe79fc3fff2b04fa54e6c6bcdba22e36791ca4dee92a662072204133eadd7290d49ccf148f24540f75bd96e71731aae61b013400f4892af76c390288293f23f43dda6c2a6eccd5541390a9cd65250dcb27099ad423850cc5856de2458fc3512e4cc4b63ce0607a8f1f15d4cf8c7c4053a874e7c419c4817291d70c554c5268814c1c5ea51263527e08cae1b3807e0f93b9d82c936885095413ddcb77131b8b2505c91c75343b4708e7e6076284bf026203a35691fd89af900449a6395cda79e368b1c90711accb371e66c936509aab0f943e4a1522aa0ae801e1ea9c766a42f7c655ca542a5b07554ff8bc22a75dcbde0d1d4cefa0311dab11f3aa435249ec1a80d74dff932438b6dfc79f80ecedfb78f53701449baa48dc4221d116fa2c332cac0db1e2c3c208ab0457614531dca92d685ad77793a5616e3f7eaaae04f806c3bdd3ffc604eaa25cff943e0e0d2680c1628ac1ebb874a4103c3999bf2b951b71b9ce3641f252aa49e0ae4edf4d153b05b60436cb48728b0f97ca31e00741f013ddb209c4eb9829731c8f8e64095bd0dc15f696fc062708e107ceedb5871992d70a37806e025a418d17adb3169e0e452ed8e573ab65306fc2413661b143effe8cca67d9e46c2d9d3b909fbc11a8d");
            sb.Append("f4c05404476d95371a0c4ca8880e6d89478cd8ff4a4e0173158cc37a9452937b6455ea90482079f832518223f1190022ccd533ef8c40e7b086e1ae7f87db85205c397b0dcc9071eda27032ed9e063bfb01af4e0723e5056e13a0bd1f82e1e378053e6ed8d36600c6e9a09add5f7cc1027fae9351adc885c494e23257e511a87b3569a2f4261e72fd57254a68ac40be28c7db5f4b65c6fd5775c363bbcaf666e812c00724904e3eaf92dbd66ffdfa745c1df6d2c673f672e03cce4ad69c2d0cf398797f6bc588d44473cb2f9a7c2fd2d44836dffc26c2a55d2253a0078ae3e454196b4be8b4838c502d723f65cdd6718292e4aabe04bff5f84835d6975c67e930210b8f852e4b9186f78954f1fd4584abc34473852d1dbc88e6c2f70643ae7529cf81a727bc37a67f6156952e333b3adc9ea790d9da55eefaf0f5c5be7dcd3a14eca385333e8f86b9d4b92901997294e27e7d1e397de2fcd7a3d7684b43e42e01ab1552f0670506301eb0d4b159725607ee6b2aa4dae9ef1d4ac1a2fd5f2cfddd013f02de179eba5ca6ad7c85922e8f474bb0e9d1c37b28fa30bb3108e30300150fe347967bf1396e27535822f49860fae2a10fa2dd99fe14ee9a030242ecc6d1225900d173af2c75cd84793f5c01262e5df05af90f2707082b77ff60ccd3883ea0b923a5153489e1405ccbeb620b5f830c174253");
            sb.Append("fdd86b53c2bef80bc89098c249904ab531dafc6e6e9d924cae3a6807ccf1a7280f43cdf290fb1c76e56ede824a62d2f1108c913689cc8449158dc6cbf356c37a51c1bdf2cdde43761af8ed04a0da70734b6a815e8d3875987b4d3631ae1f066d86cbe5c177ac44743c42f9c55ca29715ba41f1aa9969ee822b7b8d85d48464c0ea3604f8946f535533dcf416cc8f736bef6e9bb9159921859f4564367e27562c3d7ed1d0b0dc034d41dacb1f659b5b631d7718a76ae04cd7ec4b933896165bd0f6197a5ca2e619799bcf7afc78ccd7034ffd19f3b90d24db4823c79370dadf2302ca1300d05c8c9eea8c17e447e661117228b16f5665ed97a4777c24f9adf8de27ddb4fa6cfcecb63fa43c3146aa1e7bd5ea3007e02a92b459e20386d8071fae362b075b26f414d823e4217dfd256f1fde954c8df35090bd87ccd89fc4865e9e6ef1b6124bb4fe58552775a8df3a36b2a83eb7b29343f9cf93092b9b850ca2d7163b74a7d4d6fceb59f0124567d732916a9511c4592b7e87ef150d31f74196ad6cc98e9517511d640f34faba9ab9ad5fc82f27b63f49cb5347cf225f2316e53df3bef8527c8f3d2730dc2adcfc78fc91172731c608aeaad813e0e75fa48eed321afacd4e7ec395481028fb2e5b74fa8181c27e58adc527f98d46df5248e4c17814bc59229c0bb1ac8ec4e08b1ddfe0354cdc1921");
            sb.Append("cd785f0d9dd625f6239e537b96f8f8889dfe9c40da976f8b6321ecd5b268829485a0f98ff1dae77aece917ab6b4f48df9acdcd478434b084568395bee9a2dfc336b5681239e641685f37a8505d3e7feb71c7d74e9192ce9fda12960858742227e19764e45ec02a033342a6efc953e243571e13276686ea6ea674239ade32d869d6d9885f8083405cbe8e13876b142f8aa7ebe24d546f1847ab57f7422806d3ec7f941ada86e8c03de22890ba0c703362fca6b7210a38ba9914f80771d1dbf44b96411d69532b483b448c2760787aa21614257d7316a214a4cc23e672ad58475067f5646b8d4b905ada76537a9a7a4cde61249f4c14e5f742301fd427ec261a7777a9a345c33577d1cf62442911feb4ffabdcc12c7915d264fb85bc33191aefe1d3fbe11b89d772a8283d23a5fc3314a6345947c67a2132ebeefc5e3d06b0db26be47a04dcc9bca6cdac776196c8392daec0dc1cc6e1303a5e5f9a95ee5040c202a120695cffb799116c906bf4d4753a2bb0a1154debeeedb84f2291fe9b6eb74fdba79a1ecd20065beae40b8bae9795a074710697a6ff9cfadc0dcc212679561a1a7935f15ccbd8154fe1f5955ee9fd9f71349529798c3b88ebaf2190e2c4ac65ababf56aaee8e756bc54885feed44014b730e2f4e534764d4350d43e70fe9218870cbb614055439e9e63967965dcea89d2cb2bc");
            sb.Append("a30d9b707c7fe096df0ca2cea558de504a56bfd03ecf2ddf71e67645b9962d800f715d06d61227464bb08c598538463091d1efa014c74c355bcbee2bbc68a6159afe70c87c0133d4a539b13131cf081d0e02a117fc827c7d059b235b34d3a27670aa5787660a08d5b4f2466b381c468420d3d9dc56a79c5b76b3b93f541c022141326be7e95f5a2fb7192a0ad11ec8fdc29fb5f9335dfbe74e8cd9dcb50fb5e41a00352fc4ccdef68e4807e29a62dc2e4576ec4bf48c627869e2b05ff2a3f62aae5c98f7a0daae30fa19c418306cd7fc7ade355b702e3c91bb18815556f0a4c935e27bb3f439eb0364d166118cd94657a792653c57a177942943dcd9909b51ec029c0cc12f5730ff902f3b6ac15155a8c5b6a09e1a29ecdabc2c0846ea23807906dc4adb38926f121958b0a1f04d26f8984ad2615b693004323798192d856c8af5d1bbb04d574fb5b292287c464c834b49bdfec01309547ab2747a56ae74afe4279cb79016551ff9725e0101127e9ad991fda2ddd882b3381be7a6aa927ccf6e1c57f13f10e391189f43a5294f08e00248286bbdd300a7e0a31a299d41bf875f4a471fba46a12f540acd181d669f8f2d1780f7599abf9f0c58e205de0070b8793477485972119eb671fb4296ad86d0930396860558a5dfcc2926ce30510b1337a2112b7a2b8307f1f69ea7c65d59c053315ad1ea");
            sb.Append("5a6c0fcd438012234307997610f396d216935fc8dc1eb090033566915afcaa777c258ae932c2a1400177a14afec14eaa57be83a4d0be09b153b3fbc524767bbe9113271163dff13c32359263c5dead82d2d23d4825385265b73f7076eefdf833d0324912697bd3d69cff0013dd5c19d2c1ce45282c684475171d30da2532dedcedac17ae2750e91baa1a83556709f2389a51cac7c228a770f495b88e6a464b51cc6cbb3427593a17d0ebe7b84cd55f632ab970e4c75391fe3f65a13b3ed2b8b1a47cbbb302cfbc8fa705feaad0e14fdb2276e2cec03b3a5d99d979367a1d69f21e43ca863a1a4dfb38c0e260818a04d60a030ccb23b49c3f7dffb6a421f93b1babb5a126b1cb90209dfee498308ae6982ba65419ce5606de8f3d6d22fbbeeb57a4064a441aeed2197e1fbc9e10d4d86856967c21abc853fa4f23a0fc865f8ff31b330e6cb2f4eb6135c3553c35bd6d46eddf38e10ead1808150541d2b72a52baf4d3d0fd44b85bd07f08eafeb2898fa66c4e6a6181fe7adb2492b68296af3a05f06a3f95887e95cd80722da9ebfb67856d78106fbae9431007337441d2d30f218cb59f99a44dada158de2aad4d7f4569bfec5044f187d6bff1dbb32032e6385a701722955de2ba47ab53ed2f219044299adb00f07022e60e819912792d45067deb2f32de733620faab4bc9da36860864fcd3496f");
            sb.Append("4efec0b1d5b60a574c39067f5b873cf1089a55872bbbe32be13fbd8207db49e403a150deae001369dea7d78e5c28703ee848bc23ca35377b6097ef429f02a1efb9414b604d5e2184204e8f04e05d8f0258b80a057852569314acdf19ac4517d889525f683e4e3031b5166d428e3baf6e95a4df18b80cf24fdfe5fc4b897659d39f1cd8ac00ff0359a45d7720225ef4a142ae79968e076dbef25f3a91e195e68fd9a100c07c019a440f965fd1598555acf2682a3c2e1758534a35a5b9fd44423bcbf5395ed4907540648e43128743797b5f0e983afab20150097383626a96a78a4aab967bc02fa9f28360030a43625da58143facfbf1e0e452d92e6cda4ce4f2bb3a30b8b89a22b0bdb57a9b0f94ce2924855ec8876ab04706a91f10b5db59fb226c8f4309697a11f150e458d528cbf7ecd014df93067a9f7b6621ded9e2a1fa171ebbd56050279d216e67ecb3f4545dc114b4d098b996ca6014310f2279be97ff57e2d5d36555fe361aa167b737793f4bad0de48a6eee359055b131d1c66b48eb173b271f1ec8214c93f6429ecab32104505612c3db9bb1741ab22bea0569d4ff2c6fdffc8aaff75a63377ebc849c74238d3643c1caf6a391ef90fb78525d9ed6421512572e21795ea0010cedf613aedfe52678983bd292b82c092a659ae90a26a4eab29175e358d4e27e117010f496f00ba2cb9");
            sb.Append("6c52dd4b1c793b6c33b570cb959596dcc4ecce6d031f937153724d73031161bec1afefb977f2148a8f6a69260ef414dc0c761d324169e4b86c31fd9b967e5a36d90c32b489a318ef1c5a741a8bf77fbfa6b6b008395a21daba796ef6f406c129ce47238e55090df67bcd5bc9b3f2297d8795d408793f3b6c8848c164f175ddb7bbce4db26561c785240578602ee999126a7222d654d68e886861103ccb79349f85243ffc5c4946ca066a2d86ff8b8e94bba39acd66d2b2add7dfe5c6e10aaefb26d2859f8f322709b19b510a726026499a7b6304faec8f2316698396e1cd6166a77f35448847c84b27fdb56cbc6fb72927043088e777155029e553607d27b68d08301d3b26e04a8fb115b789d1e154f92d3cf252dc2062eaadf638c5a9ea3f9633ee993b3b228b88ce2eb17dadf5eee1c709570ead6b2f3a47eec571192d1496fbc35cc3602c8e6e87c2c457c52069fa2ab6d8aacec6edf883ce5a65d8166a238746edb45b826ff72befc4fd45a77d0b4de241535e62b3b3ba225be77006ca7a211055e3f67dc5112fc2c290d61b4cdfb82772cc598498ab26be57766854dce32b5ffec369e6f26709ddfb27a0fb029f48402cbb5e37ec72ccb5d15125ff993b1a6e6e4dcbdbb05cb24ac30638bf869867ac1fda4b347a0a5a056d0e3283785119fb32a37fd7158488b6078a79de0e607d0c8edc");
            sb.Append("489936b482145670e25b954e83d39a50d6cd0ac32df6baf5a1a7cebf9182432f33b0949fb78ac929df1d3d1792e55632fc040cf307fd6c1117848828637e3dadfac350474102a963b397c072ec42abd0f18dc729e3b0e3af21183e7c731d1cbdef3a167b3963fe8fbebb84ee564c3d5b5e23d53d7760875722ff6b0e056804a892d89d5ddfcad506483edb063920e802ddf9506df2a16778eb03a64e2b48bd94c438093bc0f61edc1ee9f87c048113087f8b9ce92e6d68ce0052cdcad9c6ca44b5d176c3a1447192986d1e3a6a6d76faf5aa8ad6dc8b440a65eaa3dfd5eb71c504835f4b6655d9928adbf8e473affa466d8da0542d35b7add9b5ff11864a6b9d93cbb5536e62a5da772da7224da248d62ba2506161dac14021a93180e51c8670a46a15dbecb8d629602722e7a4213f4b2e8afaa442d4bb598c43ba4d8c3b0cb2b92daad487dd54488e86b68b986343efbd99c9e9e35cb70db824cc9087f80b84ade1052bd10c09db17b9a3d6271e9b3ad22dc69f80bb525764935156e094956345786a4f8d98231ca0ab596eab9c037d1880a2192e3e015e74ac338886320746464168c3806eb4a08b332761bd9403b6519dca6805b052d82647e857fe7a4e126c31e8c67e8a4f3f910820debad09f3d61e78e0e8dd6cc212574faedcaf6bf35606ad76e21682810b8ccafbd6dd8fb27d5c02f52");
            sb.Append("5df92522935a7ebc1b851975834a3e4a81fbc4350f3d48c634b82d04270eda89cdf4e71323f1011fd0bb1e4ca42a9624bc47cb0e142049d9057b5468d36b2dc77c142fd87e734c9c0b3c36e779ac00c56e6e26050d7093c543746ee9b4a845301a0e8cc21800cc7e52af13bc327dad3e16e911252767284f6da1548be1e5d3b523592c4d540c5eec493607ecba6d6b3161e86348fd63e32ac18ae70c3ae63fca3794e4235b9085b939669b2034197551a70debb082bd3d989b70eb5b5ec3bd5f0e978109d0e597cc7e806e19c119052e97ad6e9c7d37509c777f1e2b67e96d2c31b8479ef2b74c7e82cbbfd893109b7382dc10cd1b242d7be82773fd3a4553937bbcabb1fa532d5dca63f5f1cc78df6965aecd4fc91828dc0a12e9f30057f7b7e24a933d326b70bf223b0f332d4fdacff98f3ca6c2c9742ade7e636a8d24160cadc70913c0c2b84c022e7e5c18971d673267004d9e491485c632c58b8cce5cc9660fe17eb32c218cf279fbbff909bb56651ba83aefcbca6c4934a12bf51ec4a71c36847680e347a2596bb8857496528de4082433367fc885f4a3f954bf6dd7737b54b62ffa75017b7a77fbb7e268023445a119fce165e31f6e5fdc17e2ee048ed92e068238df920d1ee74048bab809cce74e9eab864ac4a2f13552d1dc60511ba5372fd82c5d024da5b4dae05aaa4b1dcc29fa06");
            sb.Append("89bf125e521776e115190e2f8fefbc59a5a6c7d87a22a6c736db8ee6c8c7d0f2163583643ebb95ede9359dea37bbb533ce053ac72eef16d926d4fcd389b14c8377dbfb62957ba27108dd665e95a1d4ca3bbafc2086c151a77f8a92a97f97d676d9686885cb78b088afd7a4b2e8740dc679efb41bdd26b3492de3e9a65758b7cf3c1772c1be2d795c354c2616118278be9513fa7100c87f5b12d022a8eca6f3041729bf9b05b8dc946007b7083c7576f76f95d550ce039c10e4d4262be23cb02c1d7a1afa677ce6ed330dcb92152b34e44732f3cf8017766b0741a0bde6553d2a9975bd045708505198167a989ab2cf2246430569af76a9bc3ac1da80a0c675c835544f734cd6ba2563ea8936b50edaa34912353f33d046fd5bc221ee89bb7a2fe550152fcb9446fa6956fc9ea4918936dd7fbefb7870fb9026c08df8b353d5806ea22b9719226d3f06dc4536db1b6b8d519ed16b6de48697fa5871d9a53c0b09c86a9ba328e9034cba7aa23bbb712860aefb39099c7c9dd12327faeba6063f951b2a57cbbd759f26566e1cdd47fab815ed6043d36692205583ab7ede42b4d34cf9c4b3e574b4d13689217c24fa3c8bf26d3f37b18dee65fd55208b05d24a1b3060b98d02b86642542603b168cc14cea5b2bc90dc8f705de4b4d13354611d316a4d4fd47c37924f826b43f522fae5e2cfbdbeb4ec");
            sb.Append("c3755d2765c4ca11be3e25718afdecefee8ae9a1ceb214013a0c5d1eae0c10c9d7aad13c14f6f3c462bc9c5b8670ec9b20faa623c6012c29daf0054d60b3ac23deda13d2d1deda25ca3b9e0b128fdfe3151273fc3536437651c973fb271e8ae642d09640e76935df1962fadd5c09a1cd149e33b6c24b0a64e5e9304f799b22f1362964958f9cfc5e17cf2aa5e4a71e5e3ecc4500fc6ba67ec9e6cdb794c6218c17eecbe7f8bbd358b176cec30875f44f2defacc71e664cf100796f300bf2060450952d861d341ddc261be0f6315d4a534f80824577d98863b6fa217778e80a4e57b6ae68435af69a7693bb87f0da1e09ffa1e7ba8e9a77a677b8694a2809083dfab37c125e6d4494ade97914bbe1d7055ecbe7b6078254763410dec8a63f0d089dbe566cac18855b3fd98cd464584582f4cb943bd639a3a024184ba6e51b166ceedd308421d99b22b16fb068a4c5418dffb80e0fdab6b516ecfc04acc7477a3025159023b272234c7e01d91e979a9e0501c37bbdcbf57f91533e39151cc2177be30415e81f36ba28e9009c76444e9a7905a3c6f3dedfa11272c2d4755c6f9c9e2c60b576dae783850b48f5988e0d75d4a6f5675d77b2ac1ada7e7057713a894a3083986757a4e688304ac7398e082961ee5d4c43833f1b4125b26e2a0829e5f40317e29ffa0349cd24b9c4d7b5bc46bb9c488351");
            sb.Append("d48c48175341b1a0cb71408e031e9cc9a305fb56dd00c79e3e2ed45674ec538585884c173cf8a51a861a7ba7c0ae014f967f077286516e138d7832550a1b8818044385fb56f3375e8e82c6ee07ad459fe9f945f5967112e6ecdb4805fee3966f24dcb40ff0695758ada7abbdd24c088648b91e39d4be110b1723d461333d18290beeab9f6e135c86b7b88df29f7b41a2516662396de5d0831af76456127fdfed39a03aa5298e23c144ce3d0f94bdfef1730b4b8ee275e322fd6e738e33ebcd340f65cc510ddf0dd70ddcd2496bede43129190df048a8666d310292664a707ae9e04b0f854b1a650574d7917f784163fc2c226e9a55ea01bba92476d8c62f86e08d896fc107e5da2369b025da7e5851be38b753ac4b6156eb78150974996cbd6c4aeae2b265264a28b5d79571e2665f0258463d793e6397432a71dff17f95450588dbb95e15c28557967e04874d083a3603e1e8befddc9dd39da06ff1f070da107fa7a4dfefc86f777fc676af8e052a59dd099ef1a2c42634495648be747a2d85637cf590ade38e8659cf9fd2b3b5b2d4ca7b82e9cea7e6a0d9701a2bf6746502d42bf63b658640707ca9b94578d29a813acefd7fbae8d4fd487ceacd3714cc8a0ef36fa51a4d87fdda762094414f25bf4151c1c46ac32a962ec8cfdec855de5cf11641fba60f2253c46c1dddfcfa18c443f9e78b");
            sb.Append("b54b7824aab2846bf1546aaccd7bdf856ddf2c69625376ae668266024378a6ce7f3a5de86ac8042a7b3f54a3caeffce5a94170da56a5baa073faca87282abb4455e5a7d4ac0d398dfffe29592330ba293256f2eb8afbf60d744f22edc8c0577831b35738d70f7ccf677bfe3fffb8ca461010b601bf203a817858b25fddcab82cd250cd7383fe5ee71dea753d02dcbf4c6e9d67bf765077eb179e5e80f6686b3d48c94c21d509522b142f933b275584e049dd893dc8db63b76138b4b5100bb2e8a72dd2dd9a3321c54657aa31b9c77f90fce24b1c3c8ffd58237d8b428f330c10929f7ecbb44e08d92dd9dc0b0ae88fd2fadbb8b5365b66609cabeaa2a690b6fe14910b33e82aa357df150bfc24275a090cee11f5c0276633ed4a1c44b1e0fec391daab47eead8ee42078baae52dc482b61bbcc4a24a35c025b0b26ea43d8458f08f2c1e7086054e20818be5cfe34366ed18c21dd34ff231a605e4dfe374a1446a5cc577fd6363ab41583b7ea724ab75a196eaba134112087bcd844d5beb7ffe6dd31c932643ff8af049c7b8cd56b43090963f68b2834d42b5943c03fb5002c6b03c52f6e828aff039b3010cc48ccd015e0037dc66f6554c752c86962a4214e97bdec2fcbb4461530e09f1905c23d881d5803d26775fbcb67e3d36f3e5ffa3accac75884dd52f334d22f87ee39806007510969e18");
            sb.Append("45b2871b068b6595d9f738025372523568fe746bda5f1d4516e048dedadeb4c4f1c083e92fb3bf99cbfc958d526be82ffad917ccbf2799a1953012892e03e8dfe5419f9e96c7469d847920fa4748ee1d6d7c1a96cfd16563f7cde0525ea65e945dd26a2bd256aed0f656b011efe592cef53ae1819b8ab7b6021fde4e6e2cdae580c8e170ff20e3d8778fc923e2c9c42145fd97cc9f98664900f4865075c1c5597f3d0371fa2c5bafe454a163120783de706a6772ea1e28dc5aa6800b448b1b1c289dcbe4512c392b34a13a8c4fc33c05918db19d908175ec9c06c91fd63783897358e6b9a61e8df83a7133c1e1dd9f46085acf9f4718c689da91ec7b8e16e90c189f353e798377549941f3e704d41a21e322f6ba15dadab90de9a5ff037a89dd564d97a7fc963c6c818b15fda891af1253ad3c2e29a11dfe3382f9140229a2ca9fe62ea131e05ac7766b9efe695cd6b331b1ca83b60f27c23272f40e2e98858a5c343a10d17eaa3b85434a7ee87902e9e8460cc196197eb141c41b93aabc66a82fdaaa022625cb500150537ba8603f9616c0586d9bf192ed7f6103a048083aadd6acfdc63953cc956597fc35a7ce483eb80706f0c3a51fe3d64b88b3b16e93e073e0a958c1229e33c8bb08bd3be41b0a4ee78fbf59994f128388738c77bf04378fa7add781d98ad01ab8f58174894f8301f67d9a");
            sb.Append("61f983cd6300c326ec3fdc14b7c530ffc110419595090badf6c40f61ed12714e2df098aff8122e9773d52df99dda5b94e6511d37869cf23e61430c37fa97e468a91a9c0654c46aa4e62e73a0b3cb3499a5e60235516c627562d4a878aa01bb06ad8b1073c91b55a0cc9858daa68a75af62a9a432edd377c7681b5a455df21c0745b7ff39c66408362ed36eb715df56781318ffc1d9fba88c72f20a9abb9bc8554b183c130407c92e91d291513d5166657a3e73795ef8cec38aedca49c6ade6ba0460d110e0c62f07cccad6af466b1b12645417133735388612c8bcbd77a53c1f49d6e2c381474f309af34b5aeac02df7ce3464731633795f7134a566630cffa573f884e7abbba97f8c3e675d4850861c1b92cdfe65d0a40ea34d0e050f8595ed386abefbfeedf14046673c1ae5e48ec1e1ceb3011bb2512330c4dc7f1030715d0b2fd1893115f3f450a63ccaa4ac12b81011eabc7644e91484d3eb1d5819e965f891b0efa68f17a5db19dc61289d1116dfa2f70d4e5c4d86f42d00435a225f89171cc5263a72bd8f739efb009a4849f1d7f7a0fa8fc64e3f05251aad6b8e417554dc29beecaaaefd002f2d4d3d54120670032c8100b1fdf93a035da2df974ccf3e6a6e61cf2cc5feeeffd6a0aafd0de9620209c62866e275a8c59b087e750601a93f4399004e5e532e5bf6869e2258c7012500a1");
            sb.Append("de0c4faeab45c267eec1a29b599a22bb9890513e4eca2c1fee414f0d67ba38c28c643a211b1be6619f21a56505f495511cc7d62b172d721d65cce540c7c9cbf972f97fedae775f0bed56741531f807d2870c1a12cccc17bebaf0a535e14f3586dbee9713e72272d63b42a8760a2c08618afc938f933fea6e808ba8906cc05bb54fb41536cae269059fb9541c5f25e0849d044054e4de7544620cc6cec37cf8f0aee8595be08d6405c54e7b50553b5cfa5961aa2b19b4cf5b0e78c51adb5a1a7fbdf18ae2daadddcc25e65e912e2aa5a5abecf6a712051e0d1089df23914abc46487f7d435e5f8a9879a3c650e79fc3deca1f311bb2dd1ec1f0f15ada7a2850ea924fea86d339bdd5a45147a6acc08bda31c02a2fce3385bfbd9a6101d5a4fe311c4b369ea07e4b94fcbad501cae16a774a2452a3d3c54259e9bc9ef400e8b31f070fe6cf74708b2a9f46a5c5f7c33f77575269473e946b3fca37c9e756e2b949eaabdbbd0d159eb1396d558c55ed65b0355b16c0f33d78042b15127b7cbb0a9a7b7828ccc60799332f16ea1a5f01f277c42f66a04c95c2bc71642e560abc20b1fb3296637e74b535d77bbdc36c08681fae416e987b58eaeac6fe40a2c5640182ea79a16e36592d17d19b2e98567a9dfb6d62761e271726de4c5612532e277784692575204262467445cad1484887b36a42057a57");
            sb.Append("5ef32589cbd7cb6f19cf8546b6a3c2b9b6a0b6484dc04654731ac5c3d43460640352d373775a023c3a9a14b3e14e923a1cca7624ddc6cf9bf9790fb3c96c622573ae82cdae64da753177343b819f4eddabb20ede0423a737ca8127c08140da58e59dc743f173ac97772592f44abd72551e30174edf10efddc63ab78339944cd29a3d5edda02ac4bc981061af7c7197b6979e24172186c4c7f14f9a3f97e84c10cd092e6c971f5bead709176d397cbc3c04672fcb701f89fafe70a3d72f307416807ad844e7da3644cab2c836235b98e5a5b946d5072920b6b9d204bf5b0a5bac7a1e293a7b05051bce2843cec2ae3d6af9a10823fc4608df7c67bd07038c6c87374fcdf901b66ad804f43160c92700d1a6eea81747e5c74dc858b80da7eeede9e2ae2f69cd95bd2a065041b6c63d90120b5559e8f58a366193da553932ca9fdf6e1fa87990f29ccebfdbcc61de56db1f562f9c3ba9ac5469d85587c609872e3520176f4e2896e8ac0754a0567ba008793c7f16c9a8c744807c69dc0d8b1fecdeb40051c636a63109fd669c607be7131e76b63a3ec2afa316799577c0d11d606316c4c11fa251806810f5bfb293baf975f9bea4c00d2ad475ba719463b00d8630af6b7390031c22887c4f2b63a9e453e0375ba0423f2eef8107defd547b9e871ce20f4a92332c410fe071421acd2673ed571d339b");
            sb.Append("014937dd07c6077b1994beb334a2bcac57d8221cd645a63019dd4cf6b06484635291fd263b23aa1e7a3bb424d6fc45315fcd4f398afe41dffabd9519b29d93f7798897636679f75c75f11604a17d5b470fd466dbdb291d14c2208dcff0b3bd5c7d6e41effae2c986b26e7ee100f166b305c56ac029de72c6bc1e65a4450c7950994644c97323d3625912eb4b5474bd2d28f5647618a88c7874b8e8eabd330e2295441b280e7741777984134b97321a2b80c7c2b97b594856fe62bca344f680dc1f5167d631ba900bdd786ab20c96fa1a192208ac203cab0c8c1be7637430fe5bd3d61001749f9df8a3614c087a48b8467ea1dcadd96ff827a2148e5b02831c28e1f89c9f0646232fa70087da6f09740b7e410a5843828b727ef96ca02de6f0be6070e57a448db0ca8d3377b03215aedccd9d8f599fe0219197b36f7a07284f03afe6ef6d46241eb3bc6f1499d7d5ffc497cb9e9b953691c5c6132497643498fa4ca974f4419a746069f03546e355aec876836c0d7de61265d192c9271af326029fc5cb05ba9f27b6659f5ba5976e7aa3e7edeafc2052254db229015fa8c12053138e4993bebd8e518d46f8587e075fb9c3b66e0952007e4fac33758465b457626f8dcf7eb99b49c8c7db68a8dc20f9c9bd5c3770ae65410b3e0ea34a3fd5669a71ed594aaaef01367a522b945464ed9fbed4320f");
            sb.Append("65a85e1049d96e728e26b954d9f65820cd3d1e917a87c04ca8a8274e25d78b1665a3fb33868f186310fb1913b284956f23d4105c9601a62651bd7de5e0e1362aad509ed9a739867c1e6203ab3469c14f5e88731c7938341ca411fc6ae3bb695093ba2e31f37e9c4197b6aec290bb5686c24b971af75ad3d0b38dc2b405629b229b4e8bf7f8d6dd0fc2ee4a5519f8a6850e636b4ad7d07009f7e85723e48def0c8bbc92a12c1fb08734cfaeecb2eafb9215a3d630f2d70d245c1149f09ddaaea414c9b8f6cdc6be0f3a60486a568644a546aee14c8b8d6f52e067a64c46a7a5fc55af6a0773101ccd42d77c94db7f744000203743206f76218e20ab025f6860149cb3e4005539e1ddcad6e52a55f0a614034f3b9ad0331851f400b347095d841ccd9a0cd6cab899323d37afa1c1a2b97ab9b47bfa5f44c25eabd63c3e7215d609e0f840e134c5f64d264739b5c177bd72d2c77ae56606f1097719089ca62522ea2dc6938a8f74bc6dbc1017d923ca2f16984eaaeb673db24d1abed7c678d1295434144e3211d2c06188f97be3d373fcdb8bbd1fbfa277a8a7c1bc3cdd9b8d06c9437adc9361d6cd9863a3253ddbe8eb9edc41ab743ef1917245d1fb10b5d54ce7e41f107b015f953da7ed669dbe879e4387eb50d2ead5d21fb347e8715dd529b7986e1b22451a5d084490ff479ddffd83a1ab9636");
            sb.Append("c414c0a8213f74ce5278d82a3faf52a966aebd03033019501d3862689930f5ad8263fff5111dfec7f7c3c6ad1b9fa00d4c735a718e0ddad8edbe755f5bb875af9a027eb293fb91157a30d55fa9f669d11198271ab7c0e4e518a79162b29a684baaeb51c13959a6ae33ffc6ddd61c8844c2a49b9122b55a3445b85bc03d366931c8455433f78bbd28713be925be24626b019d4001c63ea9b5392e2dbcaa3e61019b86fccb767d934d54d9130e4aa32fb8a543016a55ef2a4554c1decbf7902982c27d518068cd4a18857a5918965a3b7a61aab9fa51abcf8b97f18e61f27a7d4166a81b1565918653385644ad2deb5e73063078dd708c6ecf00eac22c9c4c3b9fc9c8663bd9c8e7327a48188aac1d4b0669eb66546aaa81d5a0ba8fa06a3a2c5e6d54247f254326c3ea768518b4440f43a79a1cbf5051790b0b47b19ac4b84947c683286b997a035efa07a706245fba34a3f8871410f92189db1ae33d8c2226493d6deb6aa4a87acd0cf75b539ab935f02363bb57197ed6ccd6ef869b42dd50a835c2fdd99aabbccd49fa9ff16f5f7919c887bc9edd459ad9cd57745fcb119a058a06d2a84dd7690535e792178ea54f76b1136047241980aab2254f30c6b19e45b30c1d2befcd9b4e406be5c06f83d8118925d84ecb5886d8c9e8677d0cc12e3c80220970c8e8dc532281725ff33ff18197358ee8");
            sb.Append("d81826dfbc7699e0cd6bdacc57117e429a8bdb935d8c3b00bb0677e4aacb15c74f6b8f690d04b23a61f6180b9a68af4eee0c8b93903bc3e47ece6ef8787a72f425af3c54ec41e984728176ef838638d4a33657f8d1e97c6bf2e71524f19656270eec81a9f638b3001f9202e156d2c9627765d94debc69ad488111de2aba2a35714120624555bc8ea3cbbd0e4cba956893a9ca2c28ff4290d26089c45f4658cd6219d960002785f421e2be55efaf586e91af425112e2e4319bf67a2040ef85be1e126f2a391aaa549cee58bbc4b8bb6916bf4fbc0fe59212d146bc2a8cc96392b123a5bbee2673e5ce983faa181abf8940b8a123c406d105b69a1bbc74493b448c6a6eea3885742ccfa9170225e386f3b77c65976b3f87d8683c74e01e61f410e92ca8d97656a7004c8e9015d83889b54f4406895d14730f53665b69ddf7a31a1083c1512afd03d24a6f83399700739f6542c36020de47fc9d36dc9b2b9e411a5d82ec024da35293dc74bc30f46027a07baade7e65e0e33c34188b9f459a8c302afbd020b869d3bb76818115037898b47db4ad46c5c087daf3d35e1ff603e197246b43b8bef9ec96b3e3c86686c5f2582ca1b7f7edd8e956b6176af55b2527da6b1255da925f078cacb29f0b4e0c8f70be0090f378038dc02a7318acdc08db81e56b8c1c4754a3a0858356f4f116fb24f5b87cde6");
            sb.Append("10282b44d5b08e13e6918b8b962fcc7e03fc76221655a91878aa7d4ba961c41e23a9e588d81659cbfaa9eea569c5f79a8dbf1136b00a947c4e21e91058eacb79a014bb0a2bfdddf3a7dbb1746c6cd247b236914e70f66312b320d9053fd5ad09f2762543a0a96b0e6d6873b3266978ff2cfc920fd653675370bcd34f27abb6943d207bce195894ee7687003b28ce26c37d30f7cd0356539a53ab1ba3eeab03159bb99cf72551af3c75c04fb666c2e58d0ebbcb40cc16278ccd1892cad992266ae694f601cf2b0a9b9360e44d35b3a4bc1dd85f3c482bc68dfdbe26ba1c281e9742fe451498de683de17aed70fcb6cdf4258dbd11020a7718f09b7aae30b518a7b97446778fe19cb522dc53895b93b55de82f86ed53c071740b8e0467a3640c2518cccce7f75b37e388ccc9347f6289b86ffea2c7ab40c6c553e3a212e3a801f4908e2fdfb9589ceefeb1d07b27c1f159e6d115b4d6866a1b3ee38bae2a342645cc49d02ccadd69fec0fa2720dd83f7c44cd61f5cb4f20265751c52fe7f53da53e61d4a589e05afae37a2e82cce1b9b0e3eaf95e96fedf17e51875273f7971f6d5e456a3d0c2905baa68e1e66d874ad92297da1133f7adef161f0720b2410130f1df22f1609a9e8ca5d289f20340caa6843d639bb90e8089c1697f4a59033de30acaec92da9be338fc10e74adb92498583a6784c1");
            sb.Append("2a6c47897674b4b38c15a1dff82e5b3df493863c45cca93566cbc1e34496a95497f47b53d6a7e534bd61a4bba21158cc4d474a6b2e64596ed364310831e9077e07a7219e8653c1cc2268e5846372430ba95847896acf0728e1bc922aaf4cee013d821563d66a6154c87ff560545619bb510063ed2a7e1176c2cf5e9ed04f904d037bc5e968617dae6d819e0ced7890c603be0b33019ee1d7b1c9998036970fb6286fd5c27d87e128381326023ae4d996cab2131183bd84147d6f30671edc0c09b2a1d49cfd2728e277e663dddca9d9296c3362a221a83ff9bb99a3df7ccc91ab5cafdba0a04250ed43d36260274e8d0dad45461efcd286aee14d1dbe61102a18e7f88c49643c3c442714afcf92036f7aca6b9517d03821893205675451b689d246b568a93810cdc2e5f655332c0821f2d39a0ac547cf3409b7c98c7d94febe4f10e6c02e32526c159d5d025bf7615c31e474b16f52f970c37449cc288653fc6e9be8f912ace8c5f1afbcd71c77c49b9a63acc18d89ccf99d2905b626280f584f7bc503f64d1eb64c104d519f1117820656333a8e67727f67189bb314bb750371fe23d13b39686a317b058176f74c3bed39bf9a71654a1c29dabeb9986cd474553138a8efaa6849d4d31c829cf3973f0deeaf54b006ff4c173eb2370a2e7069482ad46e31c1b84bc5a602bffb1f892752b9bffdb6");
            sb.Append("13cf04dd368226a40880a51332ac64333be02ff8ad92af3c1d99aa2280a1f3c7b1b17151b168e738c3a5458b05d1a8f2c2aec0da7392daa4381db2da45260664a79774cca5420b699c5ddfe1c2ced2a6995494fcf3297451a9c760155d86f266b58b82cb421ccd84affad211cfda8fc3823880d7f4f96cfd7031be4539633b8d7abdd6702890bfd97de77d214ec88583442a90827b0c51ec2fafd2765f69e3722293ff90d94f519ac3d3b8b7627b02175d1f0c343d9ff2d235e5ba7efb131dd945ef166de83ee4d84d936fa4b009fcbe74241be23565574b3a3779c1e31b1312c3dad2b249ee0b67242aa51467c258cda0040c891e7649afc44de0d1d73a5728786ffd68ba33c9863a0181b14c0ac2d2becb30f27c109845565a878c9f3c69ea36145ebe13bc4d59db463ba734bae746622ea2e8a8e8d1e13fb581adfe20d9f790260c354112a489e18f99765e44ea798ae477f77ec52c6af1dd96c7964fd97de42f466c3d6c8db5311c2b0fe63dcb1990aab35d4f819b03bd1c4828021fc96e81c34c18d0ac8d56dad524aeb4fe6c0d1dfb789c96e730f2021b3846513b9ec150cad92da8e0193482564b1d37dca559c80f14cdc17284579fabbf18b23eceb1667140edcc81be23a3db03e8f32c839c327fbe12c7a0af8657e5c3dea150c1abc2e18ba03a0026f988c41f8f9a1be08ba8c256f6");
            sb.Append("4969db6d2a51b931ca7a2723960ee993b4780f5838cd2b6b4cb5fa8905cf8a99284be4329009611a7487947fdc5d790a9bff91b6d5e044fcf6f7a61e31c558735d3f71df7998e862fc839399c9d1cba2eb2bb6d1955a0ab06de1f44a30026facba4b02e1fe89e37859c2db2eb3a0f5bb999ac9fd7133df7084640f7f7475da58ae6e1892eb8937d17572b88bec9b71276a39806756447e95c35dae7e3ab0f5af10f815b3450a630c13cadb108a425430f592c3bf43bd9f56afd2c4765a957595e077f090f30de04c3decb1492ea138c46465f25d2ea1331ca7f36d7fd5b2b8dccc440b580dba8eb4988b97fe28b2d7fe79b1a6679759f92705fe2dd7e6b2757a03464b72ad5e5efd67ba3d3101a699fae7f25ca804bbcaadf8309e0b24d6f5b084ec4d786f24c2fe76915dda5eec58a09a2f6b682430e0f2c4f273dc292f72e539a510a7861b2c15e9c44b10b5a019ad0c610653cbe41000991a04f295ad1ac4339931a89436a1245b57f2692f267e863d68c0ac7185a094ffda8d790cc555186cba3850f778e86615c1f24b098ed7961dc2658c58ccbcf39a9bf7513c5f8c341921f19b2bcd0d18cf7482cbc0dc6ca210d1c9e3f2b262219ff7486caa1857ef6ff113f1bdbc317e69f9683dd0a60151aea6df8a41fbceab28a26d247c50bcfeda5e4a1e0630e432f04d964e9f0d6588a0892f73");
            sb.Append("db4ca4a26275654c21ae65bcb37559554e4383b156ab226403e1cabe64b54aba88ffbb65fa7217772562a564826443a649d173be8570ce0db2704d4f37c4188c0895429f5c4034d20142b9d473aae9dede8251e0082a947b82bb07938ae0c7d352cd751171144ab396cb1389406c1105034bdc04b108f6bdae4b0dbb4ea19950b6e7f629f058ae38c7c2ba182d763c7b7f024af9cc4db99e5e359765aeb82df31a73e4ac765413b5bc45c8bade4237330318116b6fd73ca7802bbc97b2cf3db5ad9c7b6d809f94674484b267388e912a381324e8378aef4094821864cd3ef9d6bbd1d248aef7e0e10bde7c481f7596cbf9f91c65361bd6a05645576fe1ce056a4f3cad45ee20c17aae320b2bb37a753075c1ee0654ffade3cf71c62ddf44c93bb7a1a53befce72625b5ef12d31e6201be1027f2db87b519d4a75588708e56ae1a5fd54de665ec4c4682887d738de129b2e6c566656b90ccb556fc75b314446869f5a4ab3025adba7559810d487ca6aa0f6b91ad79fe117e0ad21fef43d9f3cf3a097fdf07d94574f75c8b035ed763d4123f8a1ca051f0cd1420d7479255188b64ed969221a9eb06b9488ccd1088ba6777cc41565f52e0f30c8ba8ebd20466df97489201779ef30ca3a726ea62b0bf6351d4bbf354d9f5fd44d41491a4af6c0156c294ed2ab8df005ba462cbc2c7ef90a90736a8c");
            sb.Append("a38c4cd11c1de8f8eff2e02c870873861197498835f520f41f0b13dc1cecc2be9cc9ae80133e6219f3cd69216a2cc58f76f2b495116d9c3fc40e5b52e7fa3e3640fe2bcf8a0e7d742e2a4246dbb65e6b08d9aa521fb5dfaa00d71a2a104c5f8b4cf9ea02f2412bd465326fd8caa1ee42c7753ac2ca83963f84cc8c01018c5f03bdd1e894a578f6027ddf844341034535916ccaa25820d5a90ad02be589d5c6b3f2f023cd493504c16fdf162e911701cff1c82da5d75682198d419a6a723ab5aaf0a1adbbd0977b77774330e47c69a9e1da03161c261f11b20694ebcd1e91aefa71bbf709c4afa6d47f515207115ec6121361c3ab1de18046cdeccd1aec0f92997cf7e0cff6dcbcd60e8d6831da298d62942331d331f95dd37d304bcbb36bdd9c866947a6195de9e1508ab4008565311883b7e637462d925611df181bac741941cf9d295b81b08561cdee816db63966be0a8a21e7aa0de308b7dd95ee54c8e094222057219380510eb8a08cebeacdde3e4e3cd48e8b23090bd440398418e8fa8ee1dcf547990d582bbefa0d5cf734c0fe71b7d5a5e607c0ee059e7f547aead89e039462131675576fa88fbc760fc01584377bff476f6ee1fa1d3ceb68433d35ac81a8ea8d788c66dcf4b0022d213aa27119a6998985f579b3d374feee198e5d713e5a4dc2391f4de05e9b2147bbf04e8fcda4d74d");
            sb.Append("8cee1f390f311621fcf2395621ef18d8a1ee2b4146fc205c4882e29a4d89fd6f0cca354228128ef8def7ddb0775adba217bf2f547c2c64bdae91bfc801578422f000417d5e535798b23bf41b63e63c55b77c288fc40033cb5f988cd8ebd2437645a926e2c4f33cb4aae4dd084a17991754f3e679e940b559305ec201bd186dfe5884e87b9eb29f6f479a1103ddf03cbd70be2a82d704575e458f8d7e5a2666356a805275b5f95832a1666b05182cf3dff7ed6e6b9d7335fa5d");
            return ParseHex(sb.ToString());
        }

        private void InstallPackDirectly()
        {
            DialogResult dr = MessageBox.Show(
                currentLang == "TR"
                    ? "Tarayıcınızdaki NFS Most Wanted profilinizin üzerine doğrudan:\n\n• $999,999,999 Nakit Para\n• 21 Adet Full Junkman (0x7F) Kariyer Aracı + 52 Uyumlu Preset\n  (Viper SRT10, M3 GTR, Corvette C6, RX-8, Supra, SLR McLaren, Gallardo, DB9, M5, Skyline vb.)\n\nyüklenecektir. Onaylıyor musunuz?"
                    : "Your browser's NFS Most Wanted profile will be directly updated with:\n\n• $999,999,999 Cash\n• 21 Full Junkman (0x7F) Career Cars + 52 Validated Presets\n  (Viper SRT10, M3 GTR, Corvette C6, RX-8, Supra, SLR McLaren, Gallardo, DB9, M5, Skyline, etc.)\n\nDo you want to proceed?",
                currentLang == "TR" ? "Onay: RACERS (21 Araç) Paketi Yüklenecek" : "Confirmation: Install RACERS (21 Cars) Pack",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (dr != DialogResult.Yes) return;

            byte[] masterKey = ParseHex(_KeyHex);
            byte[] nonce = ParseHex(_NonceHex);
            byte[] encPayload = AssembleEncryptedPayload();
            byte[] rleBytes = ModSubStreamDecrypt(encPayload, masterKey, nonce);
            byte[] rawCombined = DecodeRle(rleBytes, _RawCombinedLen);

            // Layout: [0..63596: RACERS] [63596..97708: 52*656 Presets] [97708..end: targets UTF-8]
            byte[] racersBytes = new byte[63596];
            Buffer.BlockCopy(rawCombined, 0, racersBytes, 0, 63596);

            string racersSha256 = "";
            string[] presetSha256 = new string[52];
            using (SHA256 sha = SHA256.Create())
            {
                racersSha256 = FormatHex(sha.ComputeHash(racersBytes));
                for (int pIdx = 0; pIdx < 52; pIdx++)
                {
                    byte[] pBuf = new byte[656];
                    Buffer.BlockCopy(rawCombined, 63596 + (pIdx * 656), pBuf, 0, 656);
                    presetSha256[pIdx] = FormatHex(sha.ComputeHash(pBuf));
                }
            }

            int targetsOffset = 63596 + (52 * 656);
            string targetsRaw = Encoding.UTF8.GetString(rawCombined, targetsOffset, rawCombined.Length - targetsOffset);
            string[] targetLines = targetsRaw.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int updatedSaveCount = 0;
            int updatedPresetCount = 0;
            List<string> foundBrowsers = new List<string>();
            string localApp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string roamApp = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            foreach (string tLine in targetLines)
            {
                string[] td = tLine.Trim().Split('|');
                if (td.Length < 3) continue;
                string bName = td[0];
                string baseRoot = td[1] == "ROAM" ? roamApp : localApp;
                string bPath = Path.Combine(baseRoot, td[2]);
                if (!Directory.Exists(bPath)) continue;

                try
                {
                    foreach (string fsDir in Directory.GetDirectories(bPath, "File System", SearchOption.AllDirectories))
                    {
                        string[] allFiles = Directory.GetFiles(fsDir, "*", SearchOption.AllDirectories);
                        Dictionary<string, int> oldPresetShaToSlot = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                        List<string> oldSaveShas = new List<string>();

                        // Pass 1: Locate and update .pms-synced.json while recording old SHA-256 hashes of RACERS & Presets
                        foreach (string fp in allFiles)
                        {
                            if (fp.Contains("\\Paths\\")) continue;
                            try
                            {
                                FileInfo fi = new FileInfo(fp);
                                if (fi.Length > 200 && fi.Length < 100000)
                                {
                                    string firstChars = "";
                                    using (StreamReader r = new StreamReader(fp, Encoding.UTF8))
                                    {
                                        char[] buf = new char[32];
                                        int n = r.Read(buf, 0, 32);
                                        firstChars = new string(buf, 0, n);
                                    }
                                    if (firstChars.StartsWith("{\"version\":1,\"account\":"))
                                    {
                                        string fullJson = File.ReadAllText(fp, Encoding.UTF8);
                                        bool jsonChanged = false;

                                        // Update any 63596-byte career save entry in .pms-synced.json
                                        string sizeMarker = "\"size\":63596,\"sha256\":\"";
                                        int sPos = 0;
                                        while ((sPos = fullJson.IndexOf(sizeMarker, sPos)) >= 0)
                                        {
                                            int shaStart = sPos + sizeMarker.Length;
                                            if (shaStart + 64 <= fullJson.Length)
                                            {
                                                string oldSha = fullJson.Substring(shaStart, 64);
                                                if (!oldSaveShas.Contains(oldSha)) oldSaveShas.Add(oldSha);
                                                fullJson = fullJson.Substring(0, shaStart) + racersSha256 + fullJson.Substring(shaStart + 64);
                                                jsonChanged = true;
                                            }
                                            sPos = shaStart + 64;
                                        }

                                        // Update all 52 Presets/XX.bin entries in .pms-synced.json
                                        for (int pIdx = 0; pIdx < 52; pIdx++)
                                        {
                                            string pMarker = "Presets/" + pIdx.ToString("00") + ".bin\",\"size\":656,\"sha256\":\"";
                                            int pPos = 0;
                                            while ((pPos = fullJson.IndexOf(pMarker, pPos)) >= 0)
                                            {
                                                int shaStart = pPos + pMarker.Length;
                                                if (shaStart + 64 <= fullJson.Length)
                                                {
                                                    string oldPSha = fullJson.Substring(shaStart, 64);
                                                    oldPresetShaToSlot[oldPSha] = pIdx;
                                                    fullJson = fullJson.Substring(0, shaStart) + presetSha256[pIdx] + fullJson.Substring(shaStart + 64);
                                                    jsonChanged = true;
                                                }
                                                pPos = shaStart + 64;
                                            }
                                        }

                                        if (jsonChanged)
                                        {
                                            File.WriteAllText(fp, fullJson, Encoding.UTF8);
                                        }
                                    }
                                }
                            }
                            catch { }
                        }

                        // Pass 2: Update 63,596-byte career save files and 656-byte preset files in OPFS
                        string fallbackSavePath = "";
                        DateTime fallbackSaveTime = DateTime.MinValue;
                        int fsSavesUpdated = 0;

                        foreach (string fp in allFiles)
                        {
                            if (fp.Contains("\\Paths\\")) continue;
                            try
                            {
                                FileInfo fi = new FileInfo(fp);
                                if (fi.Length == 63596)
                                {
                                    byte[] fb = File.ReadAllBytes(fp);
                                    if (fb[0] == 0x32 && fb[1] == 0x30 && fb[2] == 0x43 && fb[3] == 0x4D)
                                    {
                                        if (fi.LastWriteTimeUtc > fallbackSaveTime)
                                        {
                                            fallbackSaveTime = fi.LastWriteTimeUtc;
                                            fallbackSavePath = fp;
                                        }

                                        string fileSha = "";
                                        using (SHA256 sha = SHA256.Create())
                                        {
                                            fileSha = FormatHex(sha.ComputeHash(fb));
                                        }

                                        string alias = Encoding.ASCII.GetString(fb, 0x5A31, 6);
                                        if (alias == "RACERS" || oldSaveShas.Contains(fileSha))
                                        {
                                            WriteStreamFile(fp, racersBytes, 0, 63596);
                                            updatedSaveCount++;
                                            fsSavesUpdated++;
                                            if (!foundBrowsers.Contains(bName)) foundBrowsers.Add(bName);
                                        }
                                    }
                                }
                                else if (fi.Length == 656)
                                {
                                    byte[] fb = File.ReadAllBytes(fp);
                                    int matchedSlot = -1;

                                    string fileSha = "";
                                    using (SHA256 sha = SHA256.Create())
                                    {
                                        fileSha = FormatHex(sha.ComputeHash(fb));
                                    }

                                    if (oldPresetShaToSlot.ContainsKey(fileSha))
                                    {
                                        matchedSlot = oldPresetShaToSlot[fileSha];
                                    }
                                    else if (fb.Length == 656)
                                    {
                                        string recHdr = Encoding.ASCII.GetString(fb, 40, 21);
                                        if (recHdr == "CUSTOMIZATION_RECORD_")
                                        {
                                            string slotStr = Encoding.ASCII.GetString(fb, 61, 2);
                                            int parsedSlot;
                                            if (int.TryParse(slotStr, out parsedSlot) && parsedSlot >= 0 && parsedSlot < 52)
                                            {
                                                matchedSlot = parsedSlot;
                                            }
                                        }
                                    }

                                    if (matchedSlot >= 0 && matchedSlot < 52)
                                    {
                                        WriteStreamFile(fp, rawCombined, 63596 + (matchedSlot * 656), 656);
                                        updatedPresetCount++;
                                        if (!foundBrowsers.Contains(bName)) foundBrowsers.Add(bName);
                                    }
                                }
                            }
                            catch { }
                        }

                        if (fsSavesUpdated == 0 && !string.IsNullOrEmpty(fallbackSavePath))
                        {
                            try
                            {
                                WriteStreamFile(fallbackSavePath, racersBytes, 0, 63596);
                                updatedSaveCount++;
                                if (!foundBrowsers.Contains(bName)) foundBrowsers.Add(bName);
                            }
                            catch { }
                        }
                    }
                }
                catch { }
            }

            if (updatedSaveCount == 0)
            {
                MessageBox.Show(
                    currentLang == "TR"
                        ? "Tarayıcı depolamasında (OPFS) aktif bir NFS Most Wanted profil kaydı bulunamadı!\n\nLütfen:\n1. Tarayıcınızda web.nfsmwo.com adresinden oyuna girip 'RACERS' adında bir profil oluşturun.\n2. Ardından bu programı tekrar çalıştırın."
                        : "No active NFS Most Wanted career save was found in your browser's OPFS storage!\n\nPlease:\n1. Open web.nfsmwo.com in your browser and create a profile named 'RACERS'.\n2. Run this installer again.",
                    currentLang == "TR" ? "Profil Kaydı Bulunamadı" : "Profile Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string bInfo = string.Join(", ", foundBrowsers.ToArray());
            MessageBox.Show(
                currentLang == "TR"
                    ? "RACERS (21 ARAÇ + $999,999,999) PAKETİ DOĞRUDAN TARAYICINIZA YÜKLENDİ!\n\n• Güncellenen Tarayıcı: " + bInfo + "\n• Güncellenen Kariyer Kaydı: " + updatedSaveCount + " adet\n• Güncellenen Araç Preset Dosyası: " + updatedPresetCount + " adet\n\nŞİMDİ NE YAPMALISINIZ?\nOyun sekmesinde yalnızca F5 tuşuna basarak sayfayı yenileyin ve RACERS profilinize girin!"
                    : "RACERS PACK (21 CARS + $999,999,999) DIRECTLY INSTALLED INTO YOUR BROWSER!\n\n• Updated Browser: " + bInfo + "\n• Updated Career Saves: " + updatedSaveCount + "\n• Updated Car Presets: " + updatedPresetCount + "\n\nWHAT TO DO NEXT:\nSimply press F5 in your browser game tab to reload and enter your RACERS profile!",
                currentLang == "TR" ? "Kurulum Tamamlandı!" : "Installation Complete!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            this.Close();
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ProfileManagerForm());
        }
    }
}
