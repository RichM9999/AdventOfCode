//https://adventofcode.com/2023/day/19
namespace AdventOfCode.Year2023
{
    using Part = (int[] xmas, long sum, bool accepted);
    using XMASValues = int[];
    using Rule = (int xmasIndex, char opCode, int compareValue, string result);

    class Day19
    {
        Dictionary<string, List<Rule>> workflows;
        List<Part> parts;

        public Day19()
        {
            workflows = [];
            parts = [];
        }

        public void Run()
        {
            var startTime = DateTime.Now;

            SetupWorkflows();
            SetupParts();
            ProcessParts();

            Console.WriteLine($"Sum of all accepted parts: {parts.Where(p => p.accepted).Sum(p => p.sum)}");
            // 432788
            Console.WriteLine($"{(DateTime.Now - startTime).TotalMilliseconds}ms");

            startTime = DateTime.Now;

            Console.WriteLine($"Distinct combinations of working part configurations: {CheckCombos([1, 1, 1, 1], [4000, 4000, 4000, 4000], "in")}");
            // 142863718918201
            Console.WriteLine($"{(DateTime.Now - startTime).TotalMilliseconds}ms");
        }

        void ProcessParts()
        {
            for (var p = 0; p  < parts.Count; p++)
            {
                var part = parts[p];
                var workflow = workflows["in"];

                if (ProcessPartWorkflow(part, workflow))
                {
                    parts[p] = (part.xmas, part.sum, true);
                }
            }
        }

        bool ProcessPartWorkflow(Part part, List<Rule> workflow)
        {
            foreach (var step in workflow) 
            {
                if (step.opCode == 'E')
                { 
                    if (step.result == "A")
                        return true;

                    if (step.result == "R")
                        return false;

                    return ProcessPartWorkflow(part, workflows[step.result]);
                }

                if (Compare(step.opCode, step.compareValue, part.xmas[step.xmasIndex]))
                {
                    if (step.result == "A")
                        return true;

                    if (step.result == "R")
                        return false;

                    return ProcessPartWorkflow(part, workflows[step.result]);
                }
            }

            return false;
        }

        bool Compare(char comparator, long compareValue, long value)
        {
            if (comparator == '>' && value > compareValue)
                return true;
            else if (comparator == '<' && value < compareValue)
                return true;
            else
                return false;
        }

        // From: https://github.com/Kezzryn/Advent-of-Code/blob/main/2023/Day%2019/Program.cs
        long CheckCombos(XMASValues startMin, XMASValues startMax, string ruleName)
        {
            XMASValues min = new int[startMin.Length];
            Array.Copy(startMin, min, startMin.Length);

            XMASValues max = new int[startMax.Length];
            Array.Copy(startMax, max, startMax.Length);

            long DoResult(string result)
            {
                if (result == "R") return 0;
                if (result == "A")
                {
                    long returnValue = 1;
                    for (int i = max.GetLowerBound(0); i <= max.GetUpperBound(0); i++)
                    {
                        returnValue *= ((max[i] - min[i]) + 1);
                    }
                    return returnValue;
                }
                return CheckCombos(min, max, result);
            }

            long returnValue = 0;
            foreach (Rule rule in workflows[ruleName])
            {
                switch (rule.opCode)
                {
                    case 'E':
                        returnValue += DoResult(rule.result);
                        break;
                    case '<':
                        int tempMax = max[rule.xmasIndex];
                        if (min[rule.xmasIndex] < rule.compareValue)
                        {
                            max[rule.xmasIndex] = int.Min(max[rule.xmasIndex], rule.compareValue - 1);
                            returnValue += DoResult(rule.result);
                        }
                        // reset and adjust to do the "else" branch on the next loop
                        max[rule.xmasIndex] = tempMax;
                        min[rule.xmasIndex] = int.Max(startMin[rule.xmasIndex], rule.compareValue);
                        break;
                    case '>':
                        int tempMin = min[rule.xmasIndex];
                        if (max[rule.xmasIndex] > rule.compareValue)
                        {
                            min[rule.xmasIndex] = int.Max(min[rule.xmasIndex], rule.compareValue + 1);
                            returnValue += DoResult(rule.result);
                        }
                        // reset and adjust to do the "else" branch on the next loop
                        min[rule.xmasIndex] = tempMin;
                        max[rule.xmasIndex] = int.Min(startMax[rule.xmasIndex], rule.compareValue);
                        break;
                    default:
                        throw new NotImplementedException($"unknown rule.opCode: {rule.opCode}");
                };
            }

            return returnValue;
        }

        void AddWorkflow(string raw)
        {
            List<Rule> instructions = [];

            var bracket = raw.IndexOf('{');

            var name = raw[..bracket];
            var instructionList = raw[(bracket + 1)..^1];

            foreach (var item in instructionList.Split(','))
            {
                Rule instruction = new();

                if (!item.Contains(':'))
                {
                    instruction.opCode = 'E';
                    instruction.result = item;
                }
                else
                {
                    var parts = item.Split(':');
                    instruction.result = parts[1];
                    instruction.xmasIndex = ("xmas").IndexOf((parts[0])[0]);
                    instruction.opCode = (parts[0])[1];
                    instruction.compareValue = int.Parse((parts[0])[2..]);
                }
                instructions.Add(instruction);
            }
            workflows.Add(name, instructions);
        }

        void AddPart(string raw)
        {
            raw = raw[1..^1];
            var xmas = raw.Split(',');
            var x = int.Parse(xmas[0].Split('=')[1]);
            var m = int.Parse(xmas[1].Split('=')[1]);
            var a = int.Parse(xmas[2].Split('=')[1]);
            var s = int.Parse(xmas[3].Split('=')[1]);

            parts.Add(([x, m, a, s], x + m + a + s, false));
        }

        void SetupWorkflows()
        {
            workflows = [];

            AddWorkflow("hz{m>3518:A,A}");
            AddWorkflow("xjq{s<700:R,x>3290:A,a>2004:R,R}");
            AddWorkflow("dn{x<1908:R,a>539:A,s>1576:R,kdn}");
            AddWorkflow("ql{m<3667:rpl,A}");
            AddWorkflow("jsd{m>1643:R,R}");
            AddWorkflow("dvq{s<1083:R,x>2321:A,A}");
            AddWorkflow("qzq{x<3660:A,a>2909:jnb,vhm}");
            AddWorkflow("pz{s>3001:jlf,zj}");
            AddWorkflow("gb{s>236:tpj,mk}");
            AddWorkflow("kgl{s<3549:nfm,a>2025:R,x>1769:A,A}");
            AddWorkflow("jkc{s>1836:A,x>1826:A,vt}");
            AddWorkflow("zn{a<471:A,s<757:lmg,th}");
            AddWorkflow("vhm{s<3774:A,a<2437:A,A}");
            AddWorkflow("gq{x>3007:ngs,xmr}");
            AddWorkflow("lz{x>314:R,a>1284:R,A}");
            AddWorkflow("gsg{m>1029:R,x>3382:R,R}");
            AddWorkflow("cqj{x<2250:A,A}");
            AddWorkflow("czh{x<2534:ntq,s>296:nvl,cq}");
            AddWorkflow("sjb{m>910:A,s>2975:A,R}");
            AddWorkflow("gss{m<3349:R,R}");
            AddWorkflow("sf{m<1613:A,x>1749:R,s<2024:R,R}");
            AddWorkflow("qd{m<2979:R,s>420:A,s<211:mmr,hc}");
            AddWorkflow("nm{m<2530:A,a<690:R,A}");
            AddWorkflow("cf{s<2923:A,A}");
            AddWorkflow("gqh{m<690:gzc,ft}");
            AddWorkflow("kxg{s<868:A,s>881:R,x<2889:R,R}");
            AddWorkflow("bqk{s>1277:rqv,a>1262:xrn,xtd}");
            AddWorkflow("jjf{s<2363:R,a>312:R,R}");
            AddWorkflow("qlv{a>1656:rp,sx}");
            AddWorkflow("czx{s<374:R,A}");
            AddWorkflow("cvm{m<3623:A,a>3090:R,a<2883:R,A}");
            AddWorkflow("np{x<57:R,R}");
            AddWorkflow("df{a>2646:tct,qgd}");
            AddWorkflow("pq{m>3886:A,s>1300:A,A}");
            AddWorkflow("gvj{x<3344:A,m<3208:A,x<3652:R,A}");
            AddWorkflow("ftk{s>183:czh,m<2812:dxs,mpj}");
            AddWorkflow("gdl{a>2242:R,A}");
            AddWorkflow("tpj{a>1952:R,x>414:A,a>1633:A,R}");
            AddWorkflow("hd{m<746:A,x>3172:A,x<2654:lk,jjf}");
            AddWorkflow("pqj{m<2823:rlq,m>3109:rj,A}");
            AddWorkflow("ttj{s<1462:mj,A}");
            AddWorkflow("vcl{x>2410:A,s>1991:A,x<2115:R,A}");
            AddWorkflow("gsd{m>2665:A,x<2552:R,s<1074:A,A}");
            AddWorkflow("dh{s<1213:A,bvg}");
            AddWorkflow("xf{s>2328:pc,s<1137:jmc,tlf}");
            AddWorkflow("jkd{x>1888:cf,x<908:kc,A}");
            AddWorkflow("nxd{m>3569:A,x<552:R,A}");
            AddWorkflow("kjv{x<2636:zt,x<3183:lkx,m>3114:qrb,pn}");
            AddWorkflow("krp{s>3241:R,m<3498:A,A}");
            AddWorkflow("smm{m<734:A,R}");
            AddWorkflow("ph{a<3213:A,m>3216:R,A}");
            AddWorkflow("nz{x<2386:dtq,s>2080:A,A}");
            AddWorkflow("sgj{a<1835:R,A}");
            AddWorkflow("qrb{a<955:cxr,m>3690:dc,tn}");
            AddWorkflow("nk{x>795:qp,A}");
            AddWorkflow("plm{m<1874:jbr,nkj}");
            AddWorkflow("lc{x>454:A,x<266:R,A}");
            AddWorkflow("jj{a>2841:A,a<2118:A,R}");
            AddWorkflow("pj{m>3173:nxd,x<414:A,hdb}");
            AddWorkflow("rh{x<3019:R,A}");
            AddWorkflow("nfm{x>1631:R,A}");
            AddWorkflow("blh{x<1029:pj,brl}");
            AddWorkflow("ppt{a<3642:R,m<1223:gsg,A}");
            AddWorkflow("hk{a<2029:hpn,s<492:zbb,jt}");
            AddWorkflow("nmj{s<207:R,m<3830:A,x<2241:A,R}");
            AddWorkflow("dl{a<871:A,m>2275:A,A}");
            AddWorkflow("cbs{s<609:vx,s<657:R,A}");
            AddWorkflow("vf{m>3366:A,a>560:R,a>296:A,A}");
            AddWorkflow("xq{a>1033:R,a<956:A,A}");
            AddWorkflow("pc{x>3394:A,x<3086:R,a>2727:A,A}");
            AddWorkflow("nnk{m>2619:R,a>3006:hpp,jkh}");
            AddWorkflow("zt{m>3191:zpb,bsq}");
            AddWorkflow("vl{x<3532:A,s>887:A,R}");
            AddWorkflow("lmg{m>3564:R,R}");
            AddWorkflow("xrf{s>425:kq,m>2828:A,a>324:nm,fsk}");
            AddWorkflow("tn{x<3644:zpz,m<3397:A,rch}");
            AddWorkflow("hf{a<1446:A,x<2942:A,x<3022:A,R}");
            AddWorkflow("bt{s<2790:lbf,a>1654:qpp,s>3238:nql,kjv}");
            AddWorkflow("gg{a>2956:gsd,m<2713:dvq,R}");
            AddWorkflow("mv{s>651:A,m<1448:A,x<146:A,A}");
            AddWorkflow("tp{m>3253:gss,x>1334:A,ms}");
            AddWorkflow("pg{x<2282:R,a<1349:vg,m>2840:R,hfl}");
            AddWorkflow("rch{x>3834:A,s<3078:R,A}");
            AddWorkflow("cxr{s>2975:plt,x>3547:R,x<3385:gr,fxd}");
            AddWorkflow("bk{m<3130:hkl,m<3476:tp,m>3712:bgc,skz}");
            AddWorkflow("xbx{a>3426:rc,s<863:sd,hq}");
            AddWorkflow("kd{s>1340:A,A}");
            AddWorkflow("bjf{a<593:R,A}");
            AddWorkflow("mhm{a>1647:A,a>1415:A,m<2372:A,A}");
            AddWorkflow("fx{a<3216:vz,srj}");
            AddWorkflow("ml{a<2860:R,R}");
            AddWorkflow("kzv{s<1301:A,R}");
            AddWorkflow("jkh{s<947:R,R}");
            AddWorkflow("pcl{m<3722:rvk,s>1134:pq,a<2815:R,A}");
            AddWorkflow("bm{m<776:A,a>3600:R,A}");
            AddWorkflow("hrt{s<674:R,x>2588:R,a>2018:R,A}");
            AddWorkflow("lqj{x<676:kfp,x>856:lt,br}");
            AddWorkflow("dhd{m<2607:R,A}");
            AddWorkflow("qcv{x<1535:kl,R}");
            AddWorkflow("szq{x>2379:hd,a>306:dn,s<1822:hkd,dns}");
            AddWorkflow("tqq{a<522:mb,R}");
            AddWorkflow("lkx{a<878:tqq,sbt}");
            AddWorkflow("vcc{x>109:R,m<1781:R,m<1933:np,R}");
            AddWorkflow("gdz{a>3532:A,A}");
            AddWorkflow("vgt{m<1215:vcl,jcg}");
            AddWorkflow("bxp{s>1485:A,s<563:R,m>1025:A,A}");
            AddWorkflow("bmd{x<2839:rxh,a>2312:nmm,s>1414:krn,bq}");
            AddWorkflow("rhg{s<152:A,R}");
            AddWorkflow("cz{a<1432:ztv,xfr}");
            AddWorkflow("vsd{a>2247:R,a>1946:R,a>1844:A,R}");
            AddWorkflow("mqt{x<3172:R,x<3581:R,x>3825:R,R}");
            AddWorkflow("tq{x<2593:R,A}");
            AddWorkflow("fj{m<3481:rh,tv}");
            AddWorkflow("gx{m>2883:R,R}");
            AddWorkflow("ch{m<517:nk,pd}");
            AddWorkflow("mbs{s>1219:A,s<1197:R,x>2230:R,R}");
            AddWorkflow("rr{s<927:A,x>1452:A,x<1372:R,R}");
            AddWorkflow("cgj{s<3063:R,A}");
            AddWorkflow("bbv{s>1112:R,R}");
            AddWorkflow("tqz{a>3803:nbb,psq}");
            AddWorkflow("ncz{m>3717:R,a<634:R,R}");
            AddWorkflow("jcg{s<2537:R,m<1343:R,A}");
            AddWorkflow("jt{x<1325:pbb,R}");
            AddWorkflow("kzb{m>3653:jqx,m>3482:gf,x>3249:xsk,rjx}");
            AddWorkflow("rqv{m<3538:zzl,s<1338:tkq,R}");
            AddWorkflow("hsb{a>1037:R,x>870:R,A}");
            AddWorkflow("jb{x<2075:A,x<2506:lxt,A}");
            AddWorkflow("vbk{a<2870:gjb,a>3277:A,dp}");
            AddWorkflow("jqs{a<1324:A,m>2852:R,a>1529:R,A}");
            AddWorkflow("qm{a>3573:A,R}");
            AddWorkflow("rzk{x<557:vmp,s<83:tvm,m<3236:mc,ncz}");
            AddWorkflow("ts{s<3733:fh,tfn}");
            AddWorkflow("nsj{a<1336:A,a>1857:A,a>1584:A,R}");
            AddWorkflow("vt{a>1102:R,m>2866:R,A}");
            AddWorkflow("zkj{x>2500:R,a<467:A,R}");
            AddWorkflow("bs{m<724:A,x>3306:fhb,x>3204:A,kdl}");
            AddWorkflow("plt{x>3572:R,m<3702:R,s<3130:R,A}");
            AddWorkflow("jfc{x>924:sf,m<1651:lz,mpg}");
            AddWorkflow("knp{m>564:A,m<232:A,s>2339:A,R}");
            AddWorkflow("qgd{a<1965:cm,A}");
            AddWorkflow("tdm{m>2827:R,m>2718:A,a>3015:R,A}");
            AddWorkflow("hgj{s>1445:zq,xrb}");
            AddWorkflow("ztv{m>1357:qsl,tpx}");
            AddWorkflow("fhb{s>1607:R,a<3027:R,A}");
            AddWorkflow("khd{a>3834:R,m>3473:R,R}");
            AddWorkflow("trc{s>512:rrk,m>2509:nd,s>326:sxh,cng}");
            AddWorkflow("hr{a>656:A,m>2850:R,x>3389:A,A}");
            AddWorkflow("rp{a>2022:R,R}");
            AddWorkflow("gjb{a<2267:A,R}");
            AddWorkflow("fd{x<1052:vtp,a<949:jrn,hk}");
            AddWorkflow("fz{a>980:R,A}");
            AddWorkflow("ccg{m>2905:R,a>2436:R,R}");
            AddWorkflow("qlg{m>1037:R,R}");
            AddWorkflow("nhh{x>2581:ql,m>3642:nmj,a>1170:kpf,jdt}");
            AddWorkflow("jlf{x>3531:R,s>3219:A,A}");
            AddWorkflow("nd{s<173:vbb,s>385:npn,bxz}");
            AddWorkflow("fhj{m<724:R,a>3762:R,x>3053:R,R}");
            AddWorkflow("tx{m>1717:A,m<1555:R,R}");
            AddWorkflow("qz{x<3152:R,m<1947:R,tz}");
            AddWorkflow("bln{m>1964:A,R}");
            AddWorkflow("lh{s<2877:xqk,R}");
            AddWorkflow("xhd{x<3255:ps,dz}");
            AddWorkflow("krn{m>1099:gm,gjk}");
            AddWorkflow("hpp{a<3169:A,s<938:R,A}");
            AddWorkflow("srj{m<3640:A,A}");
            AddWorkflow("fgh{m>1756:R,m>1722:R,s>1813:A,R}");
            AddWorkflow("grd{x<1684:A,x>2484:A,x<2179:R,A}");
            AddWorkflow("mpj{a<3142:A,x<2258:tpz,R}");
            AddWorkflow("jv{x>3146:sq,s>269:R,cb}");
            AddWorkflow("vc{a<1112:A,m<1598:jng,m<1694:A,fgh}");
            AddWorkflow("tct{a>3518:R,jp}");
            AddWorkflow("szk{x>2702:A,s>821:A,x<2020:A,R}");
            AddWorkflow("tt{x<3002:A,x<3058:R,s<2845:R,A}");
            AddWorkflow("xv{a<1832:A,s<1237:A,s<1251:A,R}");
            AddWorkflow("vrq{a<1062:jtr,R}");
            AddWorkflow("fbz{m>2837:A,A}");
            AddWorkflow("ms{m<3212:A,A}");
            AddWorkflow("dz{x>3696:dk,m<3179:A,A}");
            AddWorkflow("sq{x<3517:A,m<1576:R,m>1616:A,R}");
            AddWorkflow("vrv{x>2073:A,m<3335:A,a>3264:R,A}");
            AddWorkflow("nx{s>284:R,A}");
            AddWorkflow("bq{a<1964:ddh,zmr}");
            AddWorkflow("ghk{a<441:R,A}");
            AddWorkflow("br{a<2694:smm,x>795:zzv,s<2123:A,R}");
            AddWorkflow("kmr{x>1717:A,a>3663:A,s<3538:R,R}");
            AddWorkflow("hc{s<318:A,R}");
            AddWorkflow("vr{a<399:A,x>2222:A,m>3016:A,A}");
            AddWorkflow("cl{m>2530:ct,x<1855:qvv,dh}");
            AddWorkflow("vmp{x>236:R,x>131:A,A}");
            AddWorkflow("vq{s>526:vb,a>2478:kf,jv}");
            AddWorkflow("mdb{a<380:R,m<2455:R,A}");
            AddWorkflow("cpb{s<1179:dzj,dj}");
            AddWorkflow("ssz{s>1128:A,R}");
            AddWorkflow("mmr{m<3107:R,R}");
            AddWorkflow("ddh{x>3498:tm,R}");
            AddWorkflow("mk{x>561:A,s>98:R,x>197:R,R}");
            AddWorkflow("jdn{a>1667:R,xlx}");
            AddWorkflow("skh{s<2941:R,a<1171:R,s<3134:A,A}");
            AddWorkflow("ktr{s>2122:rk,a<757:tsm,vrq}");
            AddWorkflow("js{x<1491:A,R}");
            AddWorkflow("jnr{s<796:R,s>1213:R,A}");
            AddWorkflow("xd{x<2458:hv,x<3129:cgj,m>2807:qbb,pz}");
            AddWorkflow("hfl{s<300:A,R}");
            AddWorkflow("zxt{s<2782:A,s<3589:A,s>3804:R,R}");
            AddWorkflow("kf{s<213:kht,s<412:nx,m>1587:A,ksp}");
            AddWorkflow("jdt{a<424:R,x<2178:R,a<690:A,tvf}");
            AddWorkflow("dns{s<2857:xbf,s<3399:llz,a>196:fdf,jsq}");
            AddWorkflow("dd{x>1491:R,s<398:A,A}");
            AddWorkflow("jmc{a<2988:xjq,s>688:A,trj}");
            AddWorkflow("hkd{a<163:jnr,R}");
            AddWorkflow("zmb{m<3165:pls,gpv}");
            AddWorkflow("mb{m<3218:A,m>3585:A,m>3436:A,R}");
            AddWorkflow("msx{x<3666:R,m<3398:R,a<3006:R,A}");
            AddWorkflow("vz{s<1252:A,A}");
            AddWorkflow("bvc{s<449:R,a>3035:A,R}");
            AddWorkflow("gz{x<3316:zcz,qzq}");
            AddWorkflow("bb{m<2381:R,x>2178:R,a<1435:R,A}");
            AddWorkflow("qch{s>2944:A,m>1485:A,s>2282:R,R}");
            AddWorkflow("fdf{a>234:R,a<219:R,m<652:R,R}");
            AddWorkflow("bqr{m<1655:A,A}");
            AddWorkflow("djn{x>2008:nt,a<993:jns,x<1955:R,sjr}");
            AddWorkflow("xbf{m<712:A,x>1977:R,A}");
            AddWorkflow("qpp{s<3361:xd,x>2233:gz,s>3593:ts,sxk}");
            AddWorkflow("srq{s<567:nhh,s<733:kzb,a<1151:nfr,fj}");
            AddWorkflow("vgz{x<2442:R,x>3163:A,s<1109:A,A}");
            AddWorkflow("spf{m>3098:A,a>1114:R,x<2359:vr,R}");
            AddWorkflow("lk{m<1007:R,m<1155:A,A}");
            AddWorkflow("cq{x<3030:R,m<3304:A,s>244:R,R}");
            AddWorkflow("hpn{m>3374:A,s<535:R,R}");
            AddWorkflow("pnr{s<564:R,a>1040:A,x>573:knx,vd}");
            AddWorkflow("cc{s>1430:mfd,A}");
            AddWorkflow("fdb{s>349:R,s>228:A,s>81:A,A}");
            AddWorkflow("cm{m<1985:R,x>2862:R,A}");
            AddWorkflow("zg{x<2013:R,s>3441:A,R}");
            AddWorkflow("th{s<768:A,s<776:R,m>3551:R,R}");
            AddWorkflow("bvt{s<883:pxk,A}");
            AddWorkflow("ps{x<2852:R,a<1334:kj,R}");
            AddWorkflow("xmr{x>2490:xs,x<2141:djn,m>2911:spf,pg}");
            AddWorkflow("tsm{s<872:A,a<426:bln,x>1500:db,A}");
            AddWorkflow("qsl{m<1825:gd,m>1995:tmb,m>1914:ktr,plm}");
            AddWorkflow("tvf{s<308:A,m<3477:A,x>2430:A,R}");
            AddWorkflow("nbb{a>3887:R,m<2823:A,khd}");
            AddWorkflow("jsq{m<806:R,a<125:R,R}");
            AddWorkflow("gm{s<2956:R,m>1252:dfc,s>3550:R,R}");
            AddWorkflow("kv{x>203:R,x<134:A,m<1920:R,R}");
            AddWorkflow("bgc{m>3875:R,ssz}");
            AddWorkflow("rxh{a<2551:vgt,jb}");
            AddWorkflow("psq{s<967:dvk,m<3358:A,R}");
            AddWorkflow("rg{x>249:R,x<137:A,A}");
            AddWorkflow("rt{a>1268:R,a<1073:A,R}");
            AddWorkflow("rcf{m<3157:A,x<3888:R,R}");
            AddWorkflow("xtd{a<718:mbs,a>915:ss,a<801:R,A}");
            AddWorkflow("tlf{m>331:A,a>2999:R,A}");
            AddWorkflow("zl{a<2924:R,a<3347:A,m>592:R,R}");
            AddWorkflow("kdl{x<3179:A,a>3060:A,A}");
            AddWorkflow("xrn{s>1236:mdq,a<2044:A,gj}");
            AddWorkflow("gj{a>2399:R,a<2219:A,s>1215:A,A}");
            AddWorkflow("fq{s>518:A,x>3344:A,s>294:A,A}");
            AddWorkflow("rb{a>3423:R,a>2952:A,x>915:R,R}");
            AddWorkflow("bg{a<433:dd,A}");
            AddWorkflow("zzv{x<828:R,s>2255:A,s>1152:R,R}");
            AddWorkflow("lp{m<3249:A,s<3008:A,m<3278:A,R}");
            AddWorkflow("cvd{a<2723:A,gdz}");
            AddWorkflow("zcz{m>3081:A,a>2862:R,a>2373:A,R}");
            AddWorkflow("qk{a<1277:R,m<3551:R,R}");
            AddWorkflow("hj{m>3271:A,x<1007:A,R}");
            AddWorkflow("tr{a<839:A,A}");
            AddWorkflow("rld{s<823:R,m<2378:R,A}");
            AddWorkflow("fzv{a<1192:mqd,a<1307:kg,nz}");
            AddWorkflow("gpv{x>1141:R,jl}");
            AddWorkflow("zbb{a>2244:ccg,x>1335:mmf,bh}");
            AddWorkflow("ss{m<3642:A,A}");
            AddWorkflow("zdd{m<2602:xt,tdm}");
            AddWorkflow("zqq{m>575:R,A}");
            AddWorkflow("zzz{a>3102:vrv,a<2800:cqj,a<2999:A,bvc}");
            AddWorkflow("kfp{x>515:ml,A}");
            AddWorkflow("dxf{a>315:R,R}");
            AddWorkflow("vb{m>1613:jsd,a>2462:szk,hsz}");
            AddWorkflow("lpk{s>1135:lht,s>989:gg,nnk}");
            AddWorkflow("jtr{m<1957:A,a<935:R,A}");
            AddWorkflow("ls{x>1788:zg,js}");
            AddWorkflow("sd{s>376:lxx,ftk}");
            AddWorkflow("ff{x>3648:R,a<3816:R,R}");
            AddWorkflow("dvk{m>3204:A,R}");
            AddWorkflow("nc{a<1279:A,x<2499:R,R}");
            AddWorkflow("dtq{x<2009:A,R}");
            AddWorkflow("rks{m<3222:R,R}");
            AddWorkflow("in{m<2131:cz,mtv}");
            AddWorkflow("jd{s>1478:td,m>1696:df,s<981:vq,vlr}");
            AddWorkflow("ct{a>879:xv,m>3005:A,nv}");
            AddWorkflow("lt{s>1942:zf,R}");
            AddWorkflow("zfj{a<1034:zkj,rhg}");
            AddWorkflow("dfc{m>1352:R,a>2004:R,x<3229:R,A}");
            AddWorkflow("vrx{m<3175:xxm,R}");
            AddWorkflow("ngs{a<867:lrj,x>3362:nj,qd}");
            AddWorkflow("szg{a>2781:A,m<3156:R,a<2714:R,R}");
            AddWorkflow("xg{m<2460:R,R}");
            AddWorkflow("hh{x>2365:R,A}");
            AddWorkflow("kg{m<614:tq,a<1245:R,x>2659:bxp,R}");
            AddWorkflow("ht{s<3063:R,x<1179:A,R}");
            AddWorkflow("gv{x<2021:R,R}");
            AddWorkflow("mqd{m<718:A,s<2597:R,qxn}");
            AddWorkflow("jng{m>1474:A,R}");
            AddWorkflow("hzf{m>655:A,A}");
            AddWorkflow("tdc{x<221:A,m>2804:R,A}");
            AddWorkflow("tpx{x<1414:ch,a<816:szq,fzv}");
            AddWorkflow("vxm{s<304:R,x<383:tdc,m<2773:mdb,A}");
            AddWorkflow("dxs{x<2545:jcn,s<100:xg,s<153:A,jmj}");
            AddWorkflow("gbb{x<2907:mvc,m<543:xf,x>3553:gqh,hjt}");
            AddWorkflow("vg{s<356:A,m<2800:A,A}");
            AddWorkflow("tkq{m>3809:A,s<1316:R,x>1546:R,R}");
            AddWorkflow("zhp{m<1606:A,a>167:A,m<1712:A,R}");
            AddWorkflow("zj{a<3026:A,s>2915:A,R}");
            AddWorkflow("rkl{m>3409:R,A}");
            AddWorkflow("rj{s>1336:A,R}");
            AddWorkflow("skz{s<1142:vgz,m>3577:R,m<3533:nc,qk}");
            AddWorkflow("bf{m>2781:zz,a>3752:A,a<3606:fdb,R}");
            AddWorkflow("zs{a<2108:R,a<2506:R,tx}");
            AddWorkflow("dj{s>1374:pzt,m>3269:bqk,s<1274:cl,sdn}");
            AddWorkflow("dk{m>2966:R,s<971:R,m<2476:R,A}");
            AddWorkflow("kdn{a<435:R,s<659:R,A}");
            AddWorkflow("tv{a>1679:vsd,m<3686:R,x<2729:R,A}");
            AddWorkflow("nfx{s<3034:A,x>3884:A,A}");
            AddWorkflow("knx{x>816:R,x<702:A,R}");
            AddWorkflow("rc{x<1712:fnm,tqz}");
            AddWorkflow("jbr{a<852:A,R}");
            AddWorkflow("cgb{a<789:A,s<1835:A,m>3623:rt,A}");
            AddWorkflow("xqk{a<1905:R,a>2223:A,a>2093:A,A}");
            AddWorkflow("rrk{s>719:rld,a<1094:cbs,qlv}");
            AddWorkflow("bvg{m<2397:R,R}");
            AddWorkflow("zb{x<3060:R,a<430:qr,csg}");
            AddWorkflow("vd{x>278:A,R}");
            AddWorkflow("qkc{a<3268:lzc,R}");
            AddWorkflow("hsz{m<1538:A,s<813:A,A}");
            AddWorkflow("fg{s>3518:R,a<3152:A,A}");
            AddWorkflow("zf{a<2679:A,x>922:R,A}");
            AddWorkflow("nfr{s<786:zn,s>828:qvz,zb}");
            AddWorkflow("lsq{s<2128:R,x<1564:A,R}");
            AddWorkflow("vlr{m>1602:kzv,s<1153:mqt,fvn}");
            AddWorkflow("pzt{a<1316:hgj,x>2214:cc,ttj}");
            AddWorkflow("fxd{a>391:R,a>191:R,s<2888:A,A}");
            AddWorkflow("nt{s>446:R,x<2086:A,A}");
            AddWorkflow("bts{a>638:R,A}");
            AddWorkflow("lmt{a<2621:tt,a<3304:cx,fhj}");
            AddWorkflow("ds{x<646:A,A}");
            AddWorkflow("mpg{m<1752:A,A}");
            AddWorkflow("tz{s<2648:A,a>3313:A,s<3179:A,A}");
            AddWorkflow("brk{a<277:A,a>456:R,A}");
            AddWorkflow("dc{m>3795:R,a>1285:cqc,m<3754:skh,A}");
            AddWorkflow("rgq{a>637:A,x>2228:R,R}");
            AddWorkflow("lht{a>3119:A,s>1330:A,R}");
            AddWorkflow("mvc{a<2677:sg,fl}");
            AddWorkflow("cng{x>2909:tc,s<113:kpq,s>224:fz,zfj}");
            AddWorkflow("qbb{s<3159:hz,s<3283:krp,msx}");
            AddWorkflow("lrj{x>3540:A,m<2947:fq,A}");
            AddWorkflow("jcn{x>1192:A,m>2569:R,A}");
            AddWorkflow("fcd{m<948:A,x<1316:ht,qch}");
            AddWorkflow("qvz{m>3696:kxg,a<538:R,tr}");
            AddWorkflow("mh{m<3314:grd,s<712:R,x<1541:A,cvm}");
            AddWorkflow("gbz{a<622:R,A}");
            AddWorkflow("ntq{s>250:A,m>3001:R,x<845:A,R}");
            AddWorkflow("qr{s<813:R,R}");
            AddWorkflow("rl{x>2198:A,x>2022:A,R}");
            AddWorkflow("cs{a<815:A,s>2641:A,m<1444:A,R}");
            AddWorkflow("jqx{a<941:A,a>1538:hrt,A}");
            AddWorkflow("vx{m<2417:R,x>3122:R,A}");
            AddWorkflow("gt{s>2144:bqr,a>368:A,x<2059:zhp,A}");
            AddWorkflow("mdq{x<1604:R,x>2419:R,s<1260:R,A}");
            AddWorkflow("gzc{m>604:hzf,zqq}");
            AddWorkflow("kpq{x<2542:bb,x>2734:nxl,a>928:R,ghk}");
            AddWorkflow("tvm{s>36:R,a<694:R,m<3038:R,R}");
            AddWorkflow("cqc{m>3736:R,a>1421:A,A}");
            AddWorkflow("cx{x>2986:R,A}");
            AddWorkflow("nxl{m>2336:R,a>1436:R,R}");
            AddWorkflow("lbf{a>1678:ks,mml}");
            AddWorkflow("mx{m<1054:A,a<2830:R,A}");
            AddWorkflow("xlx{m>2271:R,a>1109:R,x<3278:R,A}");
            AddWorkflow("xs{x<2732:A,m>3031:czx,x<2854:gzj,R}");
            AddWorkflow("km{x<2607:R,a>927:R,gvj}");
            AddWorkflow("pf{x>1306:rr,mx}");
            AddWorkflow("qj{m<2324:brk,R}");
            AddWorkflow("jp{a>3169:R,s<612:R,a>2848:A,A}");
            AddWorkflow("nmm{a<3403:sr,s<2153:sc,ppt}");
            AddWorkflow("rpl{s>208:R,R}");
            AddWorkflow("gjk{m>958:qlg,sjb}");
            AddWorkflow("mc{s<140:R,s>158:A,a>577:R,A}");
            AddWorkflow("qvv{s>1224:A,hsb}");
            AddWorkflow("cb{m>1607:R,R}");
            AddWorkflow("nvl{x<3257:R,a<3099:A,A}");
            AddWorkflow("mmf{m<2967:A,A}");
            AddWorkflow("kpf{s>198:rl,R}");
            AddWorkflow("zmr{x<3388:vqn,R}");
            AddWorkflow("rv{s<3061:R,R}");
            AddWorkflow("nl{s<1931:pf,fcd}");
            AddWorkflow("xrb{x<1432:R,A}");
            AddWorkflow("kc{m<2063:R,A}");
            AddWorkflow("rvk{s<1178:A,R}");
            AddWorkflow("kfx{s>1262:R,x<1201:R,A}");
            AddWorkflow("fb{s>2493:A,s>995:rg,m>1647:kv,mv}");
            AddWorkflow("pls{x<1059:ds,s<3787:R,x<1733:A,fqj}");
            AddWorkflow("sr{a>2831:R,s<1861:vl,R}");
            AddWorkflow("bxz{s<250:dhd,A}");
            AddWorkflow("nhx{m>3054:R,R}");
            AddWorkflow("lhq{m>650:R,x>2996:zl,s<1699:R,R}");
            AddWorkflow("zz{x>740:A,m>3478:A,s>359:R,R}");
            AddWorkflow("zd{s<2863:knp,R}");
            AddWorkflow("sl{a>3038:qvb,xj}");
            AddWorkflow("bh{x<1198:R,m<2785:A,R}");
            AddWorkflow("tpz{m<3568:R,R}");
            AddWorkflow("vtp{s>401:pnr,a>1078:gb,s<174:rzk,vxm}");
            AddWorkflow("sp{m<1518:cs,s<2529:R,m<1688:A,A}");
            AddWorkflow("xx{x>3480:R,x>3304:R,R}");
            AddWorkflow("gzj{m<2860:A,a>1265:R,A}");
            AddWorkflow("fnm{s>574:hj,bf}");
            AddWorkflow("ksp{s>456:A,R}");
            AddWorkflow("pxk{a>3069:R,s>481:A,R}");
            AddWorkflow("xxm{x>2290:R,A}");
            AddWorkflow("csg{a>838:R,A}");
            AddWorkflow("pn{m<2473:sqj,a<1054:lts,qg}");
            AddWorkflow("bxt{m<3605:A,s>674:A,R}");
            AddWorkflow("kvs{s<3401:km,rks}");
            AddWorkflow("vqn{m<1234:R,s<899:A,m<1365:R,R}");
            AddWorkflow("dzj{s>1078:bk,x>2290:xhd,blh}");
            AddWorkflow("hgz{a<561:A,s<3039:R,x>1054:R,R}");
            AddWorkflow("jns{s>319:A,m>2997:R,s<151:A,A}");
            AddWorkflow("kj{m>3210:A,m>2599:R,A}");
            AddWorkflow("jnb{x>3800:R,a>3516:A,R}");
            AddWorkflow("pgg{x>1354:kd,m>2551:gbz,x>763:A,lc}");
            AddWorkflow("sdn{a<991:pgg,s<1308:kzl,pqj}");
            AddWorkflow("lxt{m<1151:R,x>2255:R,s>2561:A,R}");
            AddWorkflow("nql{s<3500:kvs,x<2601:zmb,hp}");
            AddWorkflow("nkj{x<1439:R,A}");
            AddWorkflow("mfd{x<3094:A,x<3593:R,m<2791:R,R}");
            AddWorkflow("npn{s<446:R,R}");
            AddWorkflow("rk{a<925:A,A}");
            AddWorkflow("fh{m<3144:R,a>2601:rb,R}");
            AddWorkflow("kq{x>1327:R,s<628:R,s<719:R,R}");
            AddWorkflow("llz{x<1865:R,x<2062:R,m<533:R,A}");
            AddWorkflow("ft{s<1983:bbv,a<2323:gqm,a<3139:zxt,bm}");
            AddWorkflow("mml{s>2364:vrx,m<3279:jkc,cgb}");
            AddWorkflow("rlq{x>2132:R,m>2557:R,a>1746:A,A}");
            AddWorkflow("prc{x>1012:nl,x>429:lqj,m>1338:nhr,dt}");
            AddWorkflow("gqm{x>3726:A,s<2694:A,m<756:R,R}");
            AddWorkflow("lvm{m<3367:A,s>598:A,x<2525:R,R}");
            AddWorkflow("db{m>1949:R,x>2355:R,A}");
            AddWorkflow("fvh{m<1036:R,A}");
            AddWorkflow("fsj{m>1663:A,x>2977:A,s>2939:R,A}");
            AddWorkflow("mtv{s>1535:bt,a>2595:xbx,s>888:cpb,zjb}");
            AddWorkflow("zq{x>2291:R,R}");
            AddWorkflow("fsk{s>265:R,m<2431:A,s>111:A,A}");
            AddWorkflow("lts{x>3595:bjf,hr}");
            AddWorkflow("tmb{s>1466:jkd,qcv}");
            AddWorkflow("xfr{x<1561:prc,m<833:gbb,m>1471:jd,bmd}");
            AddWorkflow("zjb{x<1868:fd,m>3209:srq,m>2729:gq,trc}");
            AddWorkflow("gf{m<3555:R,x<2969:bxt,m<3599:A,R}");
            AddWorkflow("hgd{x<3424:jqs,x<3784:hcv,rcf}");
            AddWorkflow("gr{m>3624:A,a>594:A,a>338:R,A}");
            AddWorkflow("hcv{a<1292:A,m>3015:R,R}");
            AddWorkflow("nj{m>3046:A,x<3685:fbz,m>2877:R,A}");
            AddWorkflow("rjx{s<635:lvm,A}");
            AddWorkflow("jmj{m<2437:A,a<3023:R,A}");
            AddWorkflow("hp{a>779:hgd,dxf}");
            AddWorkflow("sqj{s<2972:R,m>2255:R,m<2212:rv,xx}");
            AddWorkflow("vbb{s<93:A,a<1485:R,R}");
            AddWorkflow("sc{x<3438:R,m<1135:A,a>3704:ff,qm}");
            AddWorkflow("brl{s<979:A,a<1569:R,s>1040:R,A}");
            AddWorkflow("hkl{a>1206:A,rgq}");
            AddWorkflow("snt{a<3037:pcl,x>2181:qkc,fx}");
            AddWorkflow("hv{m<2950:R,sdv}");
            AddWorkflow("mj{a<1926:A,A}");
            AddWorkflow("tm{s>687:R,A}");
            AddWorkflow("sxh{x>2873:jdn,a>1033:mhm,a<613:qj,dl}");
            AddWorkflow("sdv{m<3613:A,R}");
            AddWorkflow("qg{s<2990:R,x<3664:gx,s<3085:nfx,R}");
            AddWorkflow("vp{s<3458:R,a<3203:R,kmr}");
            AddWorkflow("bsq{s<3005:R,A}");
            AddWorkflow("qp{s<1699:A,A}");
            AddWorkflow("jl{s>3798:A,R}");
            AddWorkflow("fvn{x>2512:A,s<1276:R,R}");
            AddWorkflow("zzl{m>3437:R,x>2355:R,R}");
            AddWorkflow("qxn{x<3126:R,m<1095:A,R}");
            AddWorkflow("pbb{m<3066:A,R}");
            AddWorkflow("ks{m<3192:cvd,lsq}");
            AddWorkflow("tfn{a>3183:nhx,A}");
            AddWorkflow("trj{a<3362:A,m>290:A,x<3632:R,A}");
            AddWorkflow("zpz{a<1354:R,s<3021:R,A}");
            AddWorkflow("kl{m>2073:A,A}");
            AddWorkflow("kht{a>3289:A,x<3178:R,A}");
            AddWorkflow("xt{a>3063:A,x<2237:A,a>2887:A,A}");
            AddWorkflow("fl{s>2668:fg,A}");
            AddWorkflow("xsk{m<3311:nsj,a<1513:R,R}");
            AddWorkflow("dt{s<2042:bvt,a>2530:zd,sgj}");
            AddWorkflow("gd{a<620:gt,x>2444:vc,a>1108:jfc,sp}");
            AddWorkflow("nv{s>1240:R,m>2836:R,a>353:A,R}");
            AddWorkflow("td{a<2465:lh,m<1779:fsj,qz}");
            AddWorkflow("hq{m<3025:lpk,m>3391:snt,sl}");
            AddWorkflow("kzl{m>2636:A,m<2420:R,R}");
            AddWorkflow("hdb{m<2705:A,m<2892:A,s<988:A,R}");
            AddWorkflow("lzc{s<1244:R,A}");
            AddWorkflow("pd{x>814:bts,fvh}");
            AddWorkflow("fqj{a>577:R,x>2263:A,x>2013:R,A}");
            AddWorkflow("hjt{x>3133:bs,s>2231:lmt,s>1243:lhq,jj}");
            AddWorkflow("tc{a<1040:R,a<2061:R,R}");
            AddWorkflow("sx{m>2430:R,a>1441:R,R}");
            AddWorkflow("jrn{m<3177:xrf,bg}");
            AddWorkflow("sjr{s>434:R,R}");
            AddWorkflow("sbt{a>1333:hf,m<2974:xq,A}");
            AddWorkflow("xj{s<1140:szg,a>2875:hh,m>3181:gv,A}");
            AddWorkflow("zpb{m>3499:hgz,m<3296:lp,s>2976:vf,rkl}");
            AddWorkflow("dp{a>3031:A,a>2953:A,R}");
            AddWorkflow("lxx{s>575:mh,m<2984:zdd,zzz}");
            AddWorkflow("sxk{x<1180:vbk,a>2579:vp,s<3501:ls,kgl}");
            AddWorkflow("qvb{s<1124:R,x<2353:kfx,ph}");
            AddWorkflow("sg{x>2195:R,m>320:gdl,a>2241:A,R}");
            AddWorkflow("nhr{a>2999:fb,x<176:vcc,zs}");
        }

        void SetupParts()
        {
            parts = [];

            AddPart("{x=1853,m=1718,a=852,s=421}");
            AddPart("{x=1856,m=768,a=800,s=33}");
            AddPart("{x=2847,m=1317,a=3464,s=932}");
            AddPart("{x=2618,m=561,a=3141,s=132}");
            AddPart("{x=148,m=476,a=2620,s=457}");
            AddPart("{x=388,m=1384,a=860,s=100}");
            AddPart("{x=1929,m=115,a=349,s=290}");
            AddPart("{x=3086,m=2861,a=1622,s=48}");
            AddPart("{x=31,m=423,a=315,s=1698}");
            AddPart("{x=174,m=907,a=49,s=122}");
            AddPart("{x=541,m=15,a=242,s=2732}");
            AddPart("{x=1552,m=95,a=350,s=1981}");
            AddPart("{x=741,m=981,a=3076,s=2421}");
            AddPart("{x=849,m=166,a=1512,s=1803}");
            AddPart("{x=13,m=1454,a=146,s=2150}");
            AddPart("{x=957,m=67,a=56,s=360}");
            AddPart("{x=243,m=368,a=1375,s=878}");
            AddPart("{x=890,m=274,a=421,s=83}");
            AddPart("{x=474,m=87,a=2601,s=767}");
            AddPart("{x=993,m=106,a=3272,s=1520}");
            AddPart("{x=102,m=295,a=545,s=2670}");
            AddPart("{x=2084,m=1274,a=2583,s=1055}");
            AddPart("{x=1440,m=57,a=2201,s=1181}");
            AddPart("{x=189,m=4,a=515,s=3434}");
            AddPart("{x=164,m=15,a=38,s=368}");
            AddPart("{x=643,m=2265,a=1169,s=1196}");
            AddPart("{x=133,m=323,a=36,s=737}");
            AddPart("{x=1924,m=859,a=590,s=268}");
            AddPart("{x=1308,m=668,a=627,s=64}");
            AddPart("{x=1277,m=1203,a=2822,s=164}");
            AddPart("{x=143,m=1445,a=1323,s=1941}");
            AddPart("{x=876,m=577,a=159,s=2538}");
            AddPart("{x=877,m=664,a=121,s=238}");
            AddPart("{x=578,m=1677,a=99,s=825}");
            AddPart("{x=94,m=697,a=629,s=786}");
            AddPart("{x=1108,m=1064,a=5,s=597}");
            AddPart("{x=416,m=2871,a=946,s=208}");
            AddPart("{x=1055,m=20,a=1258,s=1102}");
            AddPart("{x=85,m=144,a=1934,s=120}");
            AddPart("{x=747,m=2995,a=841,s=809}");
            AddPart("{x=205,m=94,a=959,s=1002}");
            AddPart("{x=251,m=1836,a=475,s=381}");
            AddPart("{x=363,m=765,a=187,s=536}");
            AddPart("{x=1823,m=1509,a=361,s=1068}");
            AddPart("{x=136,m=765,a=260,s=899}");
            AddPart("{x=1752,m=178,a=310,s=227}");
            AddPart("{x=318,m=617,a=1396,s=564}");
            AddPart("{x=1371,m=196,a=2487,s=2149}");
            AddPart("{x=2723,m=926,a=1502,s=1746}");
            AddPart("{x=146,m=448,a=181,s=2032}");
            AddPart("{x=1501,m=2536,a=1073,s=476}");
            AddPart("{x=850,m=900,a=29,s=2148}");
            AddPart("{x=984,m=128,a=1750,s=1273}");
            AddPart("{x=65,m=740,a=648,s=1147}");
            AddPart("{x=660,m=1068,a=1249,s=1061}");
            AddPart("{x=289,m=1612,a=710,s=1181}");
            AddPart("{x=1872,m=258,a=1788,s=508}");
            AddPart("{x=492,m=1178,a=125,s=618}");
            AddPart("{x=849,m=1586,a=3172,s=1776}");
            AddPart("{x=398,m=1499,a=737,s=645}");
            AddPart("{x=115,m=227,a=154,s=622}");
            AddPart("{x=415,m=505,a=388,s=338}");
            AddPart("{x=104,m=1398,a=921,s=164}");
            AddPart("{x=421,m=1201,a=3389,s=456}");
            AddPart("{x=661,m=237,a=292,s=283}");
            AddPart("{x=143,m=845,a=1391,s=1900}");
            AddPart("{x=777,m=141,a=207,s=3834}");
            AddPart("{x=780,m=927,a=62,s=430}");
            AddPart("{x=2947,m=1361,a=5,s=50}");
            AddPart("{x=3493,m=134,a=1163,s=2043}");
            AddPart("{x=135,m=456,a=58,s=1093}");
            AddPart("{x=1244,m=758,a=450,s=1089}");
            AddPart("{x=290,m=2583,a=1692,s=1164}");
            AddPart("{x=200,m=713,a=192,s=2113}");
            AddPart("{x=1164,m=57,a=3464,s=2020}");
            AddPart("{x=1023,m=137,a=1328,s=1460}");
            AddPart("{x=612,m=597,a=101,s=427}");
            AddPart("{x=2247,m=891,a=1224,s=2817}");
            AddPart("{x=870,m=3099,a=1275,s=305}");
            AddPart("{x=2099,m=1353,a=1867,s=55}");
            AddPart("{x=264,m=153,a=2560,s=1307}");
            AddPart("{x=274,m=125,a=167,s=27}");
            AddPart("{x=208,m=88,a=1676,s=1450}");
            AddPart("{x=5,m=161,a=3312,s=1403}");
            AddPart("{x=2294,m=1021,a=4,s=1766}");
            AddPart("{x=2683,m=91,a=441,s=2454}");
            AddPart("{x=503,m=1775,a=492,s=2809}");
            AddPart("{x=517,m=19,a=609,s=1051}");
            AddPart("{x=603,m=313,a=211,s=2889}");
            AddPart("{x=1663,m=342,a=1651,s=1501}");
            AddPart("{x=305,m=1190,a=232,s=1049}");
            AddPart("{x=3469,m=86,a=883,s=1897}");
            AddPart("{x=654,m=1526,a=741,s=186}");
            AddPart("{x=2611,m=959,a=541,s=1131}");
            AddPart("{x=3122,m=827,a=1701,s=1953}");
            AddPart("{x=594,m=1973,a=475,s=191}");
            AddPart("{x=134,m=1169,a=125,s=554}");
            AddPart("{x=4,m=401,a=176,s=3228}");
            AddPart("{x=2978,m=7,a=309,s=3088}");
            AddPart("{x=1187,m=2241,a=221,s=185}");
            AddPart("{x=666,m=1214,a=144,s=197}");
            AddPart("{x=176,m=30,a=208,s=3302}");
            AddPart("{x=1581,m=2530,a=596,s=244}");
            AddPart("{x=1264,m=1345,a=16,s=53}");
            AddPart("{x=122,m=424,a=2194,s=3612}");
            AddPart("{x=859,m=7,a=8,s=2258}");
            AddPart("{x=1346,m=211,a=113,s=34}");
            AddPart("{x=663,m=1448,a=2323,s=1344}");
            AddPart("{x=112,m=141,a=708,s=2131}");
            AddPart("{x=312,m=1345,a=1836,s=337}");
            AddPart("{x=5,m=1556,a=1244,s=848}");
            AddPart("{x=1589,m=233,a=21,s=933}");
            AddPart("{x=2173,m=2390,a=180,s=864}");
            AddPart("{x=1645,m=1798,a=773,s=297}");
            AddPart("{x=3292,m=109,a=1124,s=613}");
            AddPart("{x=482,m=1353,a=784,s=3347}");
            AddPart("{x=1189,m=3164,a=1874,s=1053}");
            AddPart("{x=495,m=431,a=831,s=35}");
            AddPart("{x=686,m=915,a=1823,s=809}");
            AddPart("{x=766,m=1004,a=1354,s=307}");
            AddPart("{x=3352,m=1047,a=471,s=148}");
            AddPart("{x=155,m=1449,a=997,s=345}");
            AddPart("{x=117,m=93,a=1355,s=14}");
            AddPart("{x=1710,m=1171,a=875,s=1402}");
            AddPart("{x=1339,m=1068,a=2676,s=354}");
            AddPart("{x=1306,m=29,a=1186,s=2010}");
            AddPart("{x=179,m=532,a=581,s=1137}");
            AddPart("{x=349,m=2778,a=1035,s=1522}");
            AddPart("{x=1779,m=831,a=91,s=447}");
            AddPart("{x=2267,m=267,a=370,s=177}");
            AddPart("{x=684,m=3595,a=349,s=55}");
            AddPart("{x=3394,m=422,a=1182,s=1468}");
            AddPart("{x=1902,m=359,a=956,s=2143}");
            AddPart("{x=3729,m=1383,a=799,s=887}");
            AddPart("{x=2182,m=855,a=1277,s=195}");
            AddPart("{x=608,m=1985,a=83,s=3923}");
            AddPart("{x=2768,m=233,a=1538,s=2232}");
            AddPart("{x=59,m=1225,a=270,s=983}");
            AddPart("{x=1804,m=2039,a=957,s=705}");
            AddPart("{x=460,m=1115,a=1049,s=376}");
            AddPart("{x=27,m=144,a=1421,s=2553}");
            AddPart("{x=838,m=2998,a=563,s=3050}");
            AddPart("{x=142,m=466,a=479,s=1125}");
            AddPart("{x=715,m=565,a=32,s=138}");
            AddPart("{x=156,m=668,a=507,s=2073}");
            AddPart("{x=1926,m=267,a=3414,s=554}");
            AddPart("{x=1253,m=1693,a=2019,s=360}");
            AddPart("{x=398,m=2354,a=1778,s=643}");
            AddPart("{x=954,m=525,a=1508,s=1607}");
            AddPart("{x=37,m=31,a=149,s=803}");
            AddPart("{x=212,m=383,a=1288,s=145}");
            AddPart("{x=39,m=639,a=364,s=1277}");
            AddPart("{x=2519,m=1272,a=31,s=2869}");
            AddPart("{x=162,m=1170,a=449,s=516}");
            AddPart("{x=2101,m=1142,a=2348,s=156}");
            AddPart("{x=915,m=2517,a=1793,s=2079}");
            AddPart("{x=540,m=856,a=2704,s=3}");
            AddPart("{x=414,m=836,a=71,s=1790}");
            AddPart("{x=2796,m=1429,a=60,s=928}");
            AddPart("{x=52,m=88,a=610,s=517}");
            AddPart("{x=1019,m=1532,a=2767,s=632}");
            AddPart("{x=1443,m=441,a=228,s=642}");
            AddPart("{x=328,m=595,a=947,s=936}");
            AddPart("{x=463,m=2650,a=104,s=3234}");
            AddPart("{x=789,m=62,a=281,s=257}");
            AddPart("{x=293,m=18,a=56,s=62}");
            AddPart("{x=9,m=665,a=556,s=426}");
            AddPart("{x=1141,m=1351,a=760,s=599}");
            AddPart("{x=370,m=839,a=102,s=1985}");
            AddPart("{x=2295,m=197,a=2136,s=181}");
            AddPart("{x=545,m=1190,a=1118,s=1373}");
            AddPart("{x=1488,m=1075,a=265,s=1678}");
            AddPart("{x=890,m=3,a=376,s=406}");
            AddPart("{x=75,m=318,a=14,s=264}");
            AddPart("{x=920,m=63,a=238,s=3394}");
            AddPart("{x=1787,m=3530,a=2762,s=5}");
            AddPart("{x=316,m=1158,a=1934,s=1069}");
            AddPart("{x=573,m=195,a=105,s=564}");
            AddPart("{x=1821,m=2141,a=579,s=808}");
            AddPart("{x=323,m=2219,a=61,s=208}");
            AddPart("{x=140,m=1375,a=46,s=2408}");
            AddPart("{x=358,m=529,a=220,s=31}");
            AddPart("{x=203,m=789,a=585,s=868}");
            AddPart("{x=2118,m=884,a=828,s=362}");
            AddPart("{x=388,m=2794,a=2062,s=372}");
            AddPart("{x=862,m=2303,a=1032,s=196}");
            AddPart("{x=725,m=1153,a=478,s=1423}");
            AddPart("{x=1353,m=313,a=2826,s=31}");
            AddPart("{x=2985,m=183,a=1256,s=734}");
            AddPart("{x=446,m=417,a=1970,s=122}");
            AddPart("{x=3598,m=2237,a=38,s=247}");
            AddPart("{x=455,m=1138,a=109,s=527}");
            AddPart("{x=697,m=1815,a=3009,s=530}");
            AddPart("{x=30,m=49,a=497,s=871}");
            AddPart("{x=866,m=400,a=1041,s=2446}");
            AddPart("{x=304,m=512,a=1530,s=194}");
            AddPart("{x=1213,m=2841,a=152,s=553}");
            AddPart("{x=477,m=547,a=534,s=2815}");
            AddPart("{x=411,m=765,a=70,s=1005}");
            AddPart("{x=1767,m=1973,a=872,s=494}");
        }
    }
}