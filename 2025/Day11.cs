//https://adventofcode.com/2025/day/11
namespace AdventOfCode.Year2025
{
    class Day11 : IDay
    {
        private Dictionary<string, List<string>> paths = [];

        public void Run()
        {
            LoadData();

            var start = DateTime.Now;

            // 607
            Console.WriteLine($"Part 1 answer: {Part1()}");
            // 506264456238938
            Console.WriteLine($"Part 2 answer: {Part2()}");

            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
        }

        private long Part1()
        {
            return FindPaths("you", "out", []);
        }

        private long Part2()
        {
            // Assumption made, based on testing input, that there are no dac->fft paths
            // So only need to multiply
            // paths from svr->fft *
            // paths from fft-> dac *
            // paths from dac -> out

            //if (FindPaths("dac", "fft", []) > 0)
            //    throw new Exception("Assumption that there are no dac->fft nodes is wrong");

            return FindPaths("svr", "fft", []) * 
                FindPaths("fft", "dac", []) * 
                FindPaths("dac", "out", []);
        }

        private long FindPaths(string start, string end, Dictionary<string, long> successful)
        {
            // Check cache if start is known to lead to end already
            if (!successful.ContainsKey(start))
            {
                if (start == end)
                {
                    // Path succeeded
                    successful[start] = 1;
                }
                else
                {
                    long count = 0;

                    // Recursively check next paths if any exist
                    foreach (var output in paths.GetValueOrDefault(start) ?? [])
                    {
                        count += FindPaths(output, end, successful);
                    }

                    // Store recursive count from start to end
                    successful[start] = count;
                }
            }

            // Return cached count from start to end
            return successful[start];
        }

        private void AddPath(string pathInfo)
        {
            var pieces = pathInfo.Split(':');

            paths.Add(pieces[0], [..pieces[1].Split(' ')[1..]]);
        }

        private void LoadData()
        {
            AddPath("zez: afn pnv uvw");
            AddPath("zae: hku ewv ydy");
            AddPath("qet: unr qfm");
            AddPath("igc: jcr");
            AddPath("vzt: dac vvp nzv");
            AddPath("bcl: kgk kns");
            AddPath("mgt: rxm");
            AddPath("jnn: hgv arg");
            AddPath("ijx: mzw");
            AddPath("crz: vot vut kcz");
            AddPath("hmv: sga nnn");
            AddPath("lll: czl rdx");
            AddPath("veg: lll ttv ymx");
            AddPath("beu: jfq own");
            AddPath("uli: efk");
            AddPath("zzv: oje xpx");
            AddPath("qpn: wag kxd");
            AddPath("arg: qmi");
            AddPath("elp: fqe nwv fbt");
            AddPath("tdq: som");
            AddPath("wci: ewv ydy");
            AddPath("odr: kps jjp odk fsu");
            AddPath("nej: sni yyr otm");
            AddPath("lcr: kns wko");
            AddPath("dtc: clh zpj");
            AddPath("qeq: ant ong wpc");
            AddPath("hgv: wjv kco");
            AddPath("cqt: you");
            AddPath("vtp: gun rwh iqr jxr");
            AddPath("ouo: efk");
            AddPath("mhr: hgo lyi jeh");
            AddPath("vha: hrj osn tjh");
            AddPath("szx: xqw");
            AddPath("chx: okn owt sbz sve");
            AddPath("vcl: kva wcg");
            AddPath("dak: svg rxg");
            AddPath("sao: hgz xhz");
            AddPath("hbw: itr wge jef");
            AddPath("xbd: out");
            AddPath("rbq: qet kaz zhk fim aky xli veg sdm");
            AddPath("faz: zba zoi");
            AddPath("wua: tol");
            AddPath("ejk: ijx rww");
            AddPath("abe: bxv");
            AddPath("tol: yqu rwu jnn rrv flx hnq cdm fns wpr lys arc pkq amt wci mgo eui zae apo rar hcp hiz kvc fdk");
            AddPath("qwv: bme");
            AddPath("rzf: ohe");
            AddPath("ajb: zpj");
            AddPath("jng: out");
            AddPath("psa: aar mof");
            AddPath("kck: ycc mpf xqw");
            AddPath("vfj: dhq");
            AddPath("nei: lif urk qwv");
            AddPath("zba: tol pmk dkl");
            AddPath("nfk: ggk");
            AddPath("gsi: dkl pmk tol");
            AddPath("okn: clh");
            AddPath("csd: vut");
            AddPath("snw: unr qfm wyl");
            AddPath("qac: odr noe dpp");
            AddPath("zhk: zrm ttk mpk");
            AddPath("ttk: jpm wig");
            AddPath("xsp: xey vrh yrr");
            AddPath("wpv: tmu");
            AddPath("hku: mfr beu spp rpw");
            AddPath("kpr: qeq svs");
            AddPath("nep: pmk");
            AddPath("zed: mpf xqw");
            AddPath("mnf: dkl tol");
            AddPath("yrj: hvh");
            AddPath("osn: efk sjn");
            AddPath("myr: uqk hfp");
            AddPath("fpq: zyb zoe cua fom oto");
            AddPath("tuu: fwr cda");
            AddPath("jjp: swv xym");
            AddPath("plh: rbq clx ikn");
            AddPath("iwe: sjn oqa");
            AddPath("sdw: qfm sao");
            AddPath("bph: jua nei jwk pjt");
            AddPath("pmk: zae rrv apo rwu flx wxl kvc hcp hnq fdk fns cdm wpr lys");
            AddPath("jei: mpf");
            AddPath("rme: vvp dac hst nzv eva");
            AddPath("rzn: out");
            AddPath("khw: noo ihr rry");
            AddPath("vot: sbv ckk vcl");
            AddPath("yni: cqt jeh lyi hgo");
            AddPath("gld: dmm ooh");
            AddPath("dgf: mpf");
            AddPath("enx: jua nei jwk pjt");
            AddPath("hqc: efk");
            AddPath("sqf: clx rbq roj ikn");
            AddPath("jml: out");
            AddPath("eeu: mzw rbq");
            AddPath("zsq: efk");
            AddPath("pgg: jza");
            AddPath("kps: xym drg vzi dch");
            AddPath("gxy: rbq mzw clx");
            AddPath("ooh: out");
            AddPath("arc: yqi lpt");
            AddPath("txu: bxi aeo syu szx qyg");
            AddPath("uyh: zyb cua");
            AddPath("cua: emo");
            AddPath("aka: fnh");
            AddPath("vst: lno loj fpy");
            AddPath("xec: xoq vzt dnz qdc");
            AddPath("wpr: arg");
            AddPath("sqh: dhq rww plh ijx");
            AddPath("tgq: out");
            AddPath("xhz: fnh mpc");
            AddPath("wpc: mmo byr oqg");
            AddPath("ufn: cqt lyi");
            AddPath("clh: xlu enx bph eby wdl xxx uaa fpq jxq brt uyh dyl xzk");
            AddPath("xzk: rje");
            AddPath("yqu: xkf ulo");
            AddPath("mof: oub fwb ghi udp tbe");
            AddPath("jua: haq urk lif qlf");
            AddPath("fta: nwf");
            AddPath("xpx: sbz sve owt okn");
            AddPath("khe: qeq");
            AddPath("bsn: kxe");
            AddPath("dgo: xqw");
            AddPath("wge: efk");
            AddPath("dpp: odk jjp");
            AddPath("hha: njk ygf");
            AddPath("sjn: kpr rtu pab diq nxk dqw");
            AddPath("bme: vtf");
            AddPath("hfp: snd");
            AddPath("zoe: ewz");
            AddPath("nzv: ksp qqv vzo scu");
            AddPath("wju: wsh mcy");
            AddPath("jfa: xhc");
            AddPath("ovn: out");
            AddPath("jeh: clh");
            AddPath("scu: dtc");
            AddPath("wxd: clx rbq mzw ikn");
            AddPath("xlu: oto cua fom");
            AddPath("anp: xey yrr");
            AddPath("mcy: zbn");
            AddPath("enz: ixa");
            AddPath("wsh: iwe");
            AddPath("sni: mpf xqw");
            AddPath("hvx: pfu");
            AddPath("tbe: clx rbq roj ikn");
            AddPath("loj: rtn egw zim");
            AddPath("zrg: vwa kgk kns akv");
            AddPath("njk: kck inl nyb jei");
            AddPath("vzo: ajb amx");
            AddPath("eud: mpf xqw ycc");
            AddPath("qqv: amx dtc ajb");
            AddPath("oto: emo epl");
            AddPath("ttv: czl wue");
            AddPath("cin: yyr sni ifu");
            AddPath("uvw: biu");
            AddPath("pkt: efk");
            AddPath("kxm: nzs");
            AddPath("qys: kzk");
            AddPath("iqr: ryc elp dru");
            AddPath("lcm: out");
            AddPath("yyr: ycc");
            AddPath("vpt: sjn efk");
            AddPath("ril: nep ezr wig");
            AddPath("wdl: vor dcu wpv yqh");
            AddPath("dch: out");
            AddPath("wue: jcr ufu");
            AddPath("nso: ijo tgq rzn");
            AddPath("ohg: mpc faz");
            AddPath("hde: mpf ycc");
            AddPath("huv: aoe zez xhc rpz qbi");
            AddPath("vrf: oqa sjn");
            AddPath("hrj: sjn oqa efk");
            AddPath("akv: fft jzl");
            AddPath("kns: jkz jzl fft");
            AddPath("brt: tdq jcb rje kxm");
            AddPath("pfu: kxe rzf btx");
            AddPath("jcb: nrb som nzs");
            AddPath("aoe: uvw jbz afn");
            AddPath("biu: dkl pmk tol");
            AddPath("xyd: beg");
            AddPath("zoi: pmk");
            AddPath("itr: oqa");
            AddPath("yjn: nic");
            AddPath("ezm: xiy wua");
            AddPath("vor: pgg");
            AddPath("qyg: ycc");
            AddPath("imv: clx mzw rbq roj ikn");
            AddPath("duh: lcn");
            AddPath("yom: cin ppc qxa");
            AddPath("unr: hgz aka");
            AddPath("xed: hbw");
            AddPath("ufu: tol");
            AddPath("nwf: ooh xbd");
            AddPath("wyl: aka xhz ohg");
            AddPath("iij: grf rwh jxr");
            AddPath("eui: tpk exk yyc");
            AddPath("upt: qac");
            AddPath("xhc: afn jbz uvw");
            AddPath("xli: zpy ttk zrm");
            AddPath("tqp: out");
            AddPath("odk: swv vzi dch xym");
            AddPath("yxk: btx");
            AddPath("iah: tmu vst pgg");
            AddPath("ifu: ycc");
            AddPath("lyn: yjn poi oyp myr yom mbk qnk wtp fkd nyi onw ylg vav");
            AddPath("efi: ycc xqw");
            AddPath("jkz: sqf jtj imv");
            AddPath("amx: you");
            AddPath("pnv: vnp biu");
            AddPath("btx: pgj twu");
            AddPath("lwf: idh mph");
            AddPath("hst: ksp qqv scu ggo");
            AddPath("swv: out");
            AddPath("ocv: xhc zez rpz qbi");
            AddPath("jza: rtn zim");
            AddPath("urk: mgt htm");
            AddPath("egw: pjw ovn");
            AddPath("wiu: gld");
            AddPath("pqa: xpx quv");
            AddPath("lfa: vut");
            AddPath("lpt: vpt uma");
            AddPath("cda: ovc");
            AddPath("ubw: mal ybz yrj");
            AddPath("pgc: zbn pkt");
            AddPath("rtn: kcv ovn");
            AddPath("idg: qmi hbw dbq kco");
            AddPath("kjr: zyb zoe oto fom");
            AddPath("mxe: sbz");
            AddPath("fpy: zim");
            AddPath("hgz: faz");
            AddPath("you: ydh mkv wdl xlu uaa fpq brt hvx aom xzk");
            AddPath("qdc: hst");
            AddPath("ahu: ppc cin");
            AddPath("apo: yqi yga");
            AddPath("agv: wol");
            AddPath("ybz: lzv hvh eud efi");
            AddPath("uqk: rkn gpk");
            AddPath("oqa: krf veu khe vdi uyk mtj anp rcw nqa pin xec nxk");
            AddPath("aky: lll ttv ymx");
            AddPath("ppc: otm yyr");
            AddPath("flx: jaq ibc cgz");
            AddPath("oxz: pgj hmv");
            AddPath("hgo: you clh");
            AddPath("wpm: ooh dmm xbd");
            AddPath("fqe: dkl");
            AddPath("vjd: sjn");
            AddPath("lcn: roj mzw");
            AddPath("gpk: hto dgo");
            AddPath("ghi: ikn mzw rbq clx");
            AddPath("jwk: haq urk qlf");
            AddPath("wcg: mzw rbq roj");
            AddPath("hnn: zzt");
            AddPath("kgd: gpk");
            AddPath("wax: pgc");
            AddPath("ydh: zui tvm oaj upt");
            AddPath("jbu: efk sjn");
            AddPath("nyb: ycc xqw");
            AddPath("qph: out");
            AddPath("nnn: lcm tqp");
            AddPath("pci: lcn imv");
            AddPath("mtj: khw srk dgk hsb");
            AddPath("rwh: elp ezm ryc");
            AddPath("zke: syu aeo szx qyg");
            AddPath("cqs: kzk");
            AddPath("myu: otm yyr ifu sni");
            AddPath("efk: ywb xec pin pab dqw nxk nqa rtu uyk mtj anp diq kpr krf veu khe xsp vdi jvn rcw");
            AddPath("hto: ycc");
            AddPath("cai: odr dpp noe");
            AddPath("hcp: ulo xkf");
            AddPath("vwa: pci");
            AddPath("rar: qpn");
            AddPath("qga: ikn roj rbq clx");
            AddPath("mob: lzv hvh eud efi");
            AddPath("bhd: quv xpx mxe oje");
            AddPath("awe: grf gun iqr rwh jxr");
            AddPath("mal: efi hvh");
            AddPath("fwb: clx mzw rbq roj");
            AddPath("mwk: nso");
            AddPath("nqa: bvt hpj");
            AddPath("idh: zzt qga eeu");
            AddPath("noe: jjp odk fsu");
            AddPath("nic: bxi syu szx qyg");
            AddPath("dbq: vrf zsq jef");
            AddPath("emo: wiu ssf");
            AddPath("gwf: out");
            AddPath("zui: qac cai");
            AddPath("jpm: tol pmk");
            AddPath("mzb: dgo zed hto hde");
            AddPath("jzl: sqf lcn");
            AddPath("ebd: jgt lwf kzk");
            AddPath("fzx: njk ixa wev nxa");
            AddPath("rnb: rxg svg bfp");
            AddPath("axf: sjn");
            AddPath("zsh: out");
            AddPath("ydy: rpw beu mfr");
            AddPath("bvt: mhr fwr ufn yni");
            AddPath("jvn: khw hsb dgk");
            AddPath("fim: lll");
            AddPath("byr: hls tbj msc");
            AddPath("tfk: mal mob");
            AddPath("efl: chx xpx oje quv");
            AddPath("aar: udp ghi fwb");
            AddPath("gwv: ejk");
            AddPath("rrv: cgz");
            AddPath("dcu: vst pgg");
            AddPath("mbk: myu qxa");
            AddPath("czl: ufu gsi");
            AddPath("dgk: noo ihr rry qim");
            AddPath("vnp: dkl tol pmk");
            AddPath("kcv: out");
            AddPath("grf: elp ryc ezm");
            AddPath("rmj: ixa wev nxa");
            AddPath("kxd: sjn");
            AddPath("bxi: ycc mpf");
            AddPath("sve: zpj you");
            AddPath("jbz: vnp mnf biu");
            AddPath("sbz: you");
            AddPath("ohe: sga nnn gzm");
            AddPath("ihr: zpj");
            AddPath("rmt: ttv lll");
            AddPath("oje: sbz");
            AddPath("nrb: idn");
            AddPath("poi: ubw tfk");
            AddPath("djn: pmk dkl");
            AddPath("spp: own sna");
            AddPath("mpc: djn zoi");
            AddPath("wtp: zke gsd");
            AddPath("ovc: zpj");
            AddPath("oks: rkn gpk mzb");
            AddPath("jfq: tjh hrj jhc");
            AddPath("hls: you zpj");
            AddPath("fnh: zba rvo");
            AddPath("afn: mnf");
            AddPath("ulo: uli wag ccf ouo kxd");
            AddPath("dbn: nso chq dzg");
            AddPath("vtf: zsh");
            AddPath("krf: hsb srk khw");
            AddPath("jxq: oaj tvm upt xyd");
            AddPath("wqo: wcg kva wxd");
            AddPath("lno: egw zim");
            AddPath("tng: plh ijx dhq");
            AddPath("uaa: tdq");
            AddPath("umc: fkd poi wtp qnk vav myr oyp ahu onw ylg");
            AddPath("ikn: snw vtp veg ocv fim ojt zhk yjk sdw rmt huv qet awe xli cuw aky jfa mbh iij");
            AddPath("qim: you zpj");
            AddPath("tjh: oqa sjn");
            AddPath("eby: eyg yxk bsn pfu");
            AddPath("aeo: xqw ycc");
            AddPath("xxx: zui upt xyd oaj");
            AddPath("veu: rme vzt dnz");
            AddPath("ezr: dkl tol pmk");
            AddPath("mzw: iij jfa mbh aky cuw awe xli huv rmt yjk sdw fim ojt ocv kaz zhk vtp veg sdm snw");
            AddPath("fkn: szx");
            AddPath("ant: byr oqg");
            AddPath("jor: vfj ejk sqh");
            AddPath("evw: chx oje mxe");
            AddPath("rdx: gsi ufu");
            AddPath("jgw: mhr fwr cda ufn yni");
            AddPath("haq: bme htm mgt");
            AddPath("sdm: iqr");
            AddPath("yrr: zzv");
            AddPath("kco: vrf jef wge zsq");
            AddPath("jcr: tol pmk");
            AddPath("ijo: out");
            AddPath("rry: you");
            AddPath("cox: aar mof");
            AddPath("zzt: roj ikn");
            AddPath("nzs: idn mwk");
            AddPath("eva: qqv ggo scu");
            AddPath("ccf: efk oqa");
            AddPath("rje: nrb nzs som");
            AddPath("dru: fqe xiy wua");
            AddPath("ycc: csd prl bcl sjz psa dak nfk ekw cox lcr zvt fzw");
            AddPath("diq: tuu jgw bvt");
            AddPath("inl: mpf xqw ycc");
            AddPath("zpj: qdk djg enx xlu kjr uyh dyl ycv fpq jxq brt ydh mkv bph uaa");
            AddPath("gux: gld");
            AddPath("lif: bme mgt");
            AddPath("sga: gwf jng vnf");
            AddPath("ywb: srk hsb");
            AddPath("xym: out");
            AddPath("clx: kaz zhk fim ojt snw veg sdm vtp huv qet sdw yjk rmt cuw aky xli iij jfa");
            AddPath("hpj: ufn yni mhr cda fwr");
            AddPath("cuw: zez aoe qbi");
            AddPath("mbl: out");
            AddPath("qlf: mgt htm");
            AddPath("nxa: dgf nyb kck");
            AddPath("dzg: rzn tgq");
            AddPath("wxl: gms jaq ibc");
            AddPath("epl: fta");
            AddPath("prl: vot vut");
            AddPath("qxa: ifu otm");
            AddPath("kcz: wqo sbv");
            AddPath("cgz: ulj kzl wju");
            AddPath("msc: you");
            AddPath("jne: njk");
            AddPath("ygf: jei");
            AddPath("cek: out");
            AddPath("tpk: hqc");
            AddPath("dmm: out");
            AddPath("vtv: oqa sjn");
            AddPath("vut: ckk vcl");
            AddPath("srk: ihr noo juw");
            AddPath("amt: tpk exk");
            AddPath("ekw: ggk aar mof");
            AddPath("fzw: mof ggk");
            AddPath("pjt: qwv qlf urk lif haq");
            AddPath("rxg: gwv qzr");
            AddPath("svr: lyn vgi umc");
            AddPath("kzl: mcy wsh pgc");
            AddPath("qbi: pnv");
            AddPath("ful: lzv");
            AddPath("hsb: ihr rry juw qim");
            AddPath("ylg: nic fkn gsd txu");
            AddPath("qdk: pfu yxk");
            AddPath("zrm: wig ezr");
            AddPath("tmu: loj lno fpy");
            AddPath("ymx: igc wue");
            AddPath("exk: jbu vjd axf");
            AddPath("ggo: ajb xfo dtc amx");
            AddPath("tqe: jml cek zsh mbl qph");
            AddPath("hiz: lpt yga yqi");
            AddPath("kaz: ril");
            AddPath("kgk: pci jkz");
            AddPath("lzv: ycc");
            AddPath("qfm: xhz ohg hgz");
            AddPath("wol: sqh ejk vfj");
            AddPath("nxk: wvz qeq svs");
            AddPath("fom: ewz kay emo epl");
            AddPath("roj: jfa yjk mbh iij snw awe ojt zhk");
            AddPath("ggk: tbe fwb");
            AddPath("rwu: arg xed hgv idg");
            AddPath("quv: okn owt sbz sve");
            AddPath("fwr: ovc lyi jeh");
            AddPath("hnq: yga yqi");
            AddPath("ryc: fbt nwv");
            AddPath("pao: lwf");
            AddPath("jaq: kzl wax");
            AddPath("ztn: hfp kgd uqk");
            AddPath("vnf: out");
            AddPath("vzi: out");
            AddPath("yqi: uma vtv vpt");
            AddPath("iqm: spp");
            AddPath("ejb: xqw mpf ycc");
            AddPath("nwv: tol");
            AddPath("pkq: tpk yyc");
            AddPath("fns: hku iqm");
            AddPath("vrh: evw pqa zzv efl");
            AddPath("wag: sjn");
            AddPath("vdi: qdc vzt rme xoq");
            AddPath("sna: jhc osn hrj");
            AddPath("sjz: wko kgk kns vwa");
            AddPath("udp: ikn mzw");
            AddPath("mbh: wyl unr qfm");
            AddPath("zbn: efk oqa sjn");
            AddPath("jef: sjn efk oqa");
            AddPath("rww: ikn clx mzw roj rbq");
            AddPath("rxm: cek mbl zsh qph");
            AddPath("oaj: qac");
            AddPath("onw: hfp kgd uqk oks");
            AddPath("ewz: gux fta");
            AddPath("vjr: kcz");
            AddPath("chq: ijo tgq");
            AddPath("jep: hha rmj jne fzx");
            AddPath("xey: efl evw bhd");
            AddPath("kvc: hku");
            AddPath("bxv: mob ful ybz mal");
            AddPath("fsu: xym drg");
            AddPath("ibc: kzl wax");
            AddPath("dhq: clx roj rbq mzw");
            AddPath("som: mwk idn dbn");
            AddPath("xkf: ouo ccf");
            AddPath("bfp: gwv");
            AddPath("qzr: vfj sqh tng");
            AddPath("drg: out");
            AddPath("pjw: out");
            AddPath("lys: cgz jaq ibc");
            AddPath("own: tjh osn");
            AddPath("wev: kck inl jei nyb");
            AddPath("dqw: wvz");
            AddPath("pin: jgw tuu bvt");
            AddPath("jgt: idh hnn");
            AddPath("vgi: ztn ffs abe poi fkd jep");
            AddPath("zim: kcv pjw ovn");
            AddPath("mfr: sna own vha jfq");
            AddPath("xiy: pmk tol");
            AddPath("rvo: tol");
            AddPath("qnk: ubw tfk");
            AddPath("pab: yrr xey");
            AddPath("rkn: hde hto zed ejb");
            AddPath("cdm: yqi lpt");
            AddPath("rtu: vzt xoq");
            AddPath("kxe: hmv pgj twu");
            AddPath("cri: svg agv");
            AddPath("lyi: clh zpj");
            AddPath("nyi: uqk");
            AddPath("mgo: qpn xkf");
            AddPath("fdk: hku ewv iqm ydy");
            AddPath("oub: ikn rbq clx");
            AddPath("owt: zpj");
            AddPath("jhc: efk");
            AddPath("beg: odr");
            AddPath("pgj: sga gzm");
            AddPath("svg: qzr gwv jor");
            AddPath("fft: imv sqf");
            AddPath("mmo: tbj hls");
            AddPath("mkv: jwk pjt");
            AddPath("syu: ycc");
            AddPath("mph: eeu gxy");
            AddPath("juw: clh zpj");
            AddPath("wig: tol");
            AddPath("fkd: rmj enz jne");
            AddPath("ong: mmo byr");
            AddPath("rcw: dgk hsb khw");
            AddPath("kay: wiu ssf");
            AddPath("qcf: tjh osn hrj jhc");
            AddPath("xfo: zpj clh you");
            AddPath("ssf: nwf wpm gld");
            AddPath("xoq: hst dac eva nzv");
            AddPath("ixa: inl jei dgf nyb");
            AddPath("tvm: cai beg qac");
            AddPath("twu: nnn");
            AddPath("zpy: jpm nep wig");
            AddPath("mpk: wig nep jpm");
            AddPath("ycv: zui oaj");
            AddPath("dkl: wxl hiz hcp fdk rrv apo jnn flx mgo eui cdm wpr yqu lys wci amt pkq");
            AddPath("oyp: zke gsd txu");
            AddPath("oqg: tbj");
            AddPath("kva: clx roj rbq mzw");
            AddPath("ojt: wyl sao qfm unr");
            AddPath("ulj: wsh");
            AddPath("vvp: ksp scu");
            AddPath("ckk: kva");
            AddPath("zvt: akv");
            AddPath("qmi: itr");
            AddPath("yga: vtv uma");
            AddPath("mpf: ebd cox nfk csd vjr lfa cqs zvt crz");
            AddPath("dnz: nzv vvp");
            AddPath("uyk: qeq");
            AddPath("yyc: hqc ujj axf");
            AddPath("svs: ant wpc");
            AddPath("sbv: wxd wcg");
            AddPath("yjk: zpy mpk ril zrm");
            AddPath("ujj: oqa");
            AddPath("zyb: emo kay epl");
            AddPath("ewv: rpw");
            AddPath("eyg: rzf oxz kxe");
            AddPath("otm: ycc xqw");
            AddPath("yqh: tmu");
            AddPath("gzm: jng tqp vnf");
            AddPath("fbt: pmk dkl");
            AddPath("snd: ejb hde");
            AddPath("jtj: clx rbq roj mzw");
            AddPath("aom: iah yqh wpv");
            AddPath("ksp: dtc");
            AddPath("kzk: mph");
            AddPath("uma: efk oqa");
            AddPath("djg: vor iah");
            AddPath("htm: rxm vtf tqe");
            AddPath("wko: duh");
            AddPath("dyl: dcu yqh iah");
            AddPath("wvz: ant wpc");
            AddPath("noo: you clh");
            AddPath("dac: vzo ggo scu ksp qqv");
            AddPath("rpw: vha jfq qcf");
            AddPath("ffs: jne fzx rmj hha enz");
            AddPath("gsd: bxi szx syu aeo");
            AddPath("vav: nej");
            AddPath("ehw: kzk lwf jgt");
            AddPath("gun: ezm");
            AddPath("jxr: dru");
            AddPath("xqw: bcl pao prl ebd sjz csd psa dak qys cri rnb cox ekw vjr cqs zrg lcr ehw crz fzw zvt");
            AddPath("gms: wax wju");
            AddPath("idn: dzg");
            AddPath("wjv: zsq");
            AddPath("tbj: you clh");
            AddPath("rpz: uvw afn jbz");
            AddPath("hvh: mpf xqw");
        }
    }
}
