using Microsoft.Z3;
using System.Diagnostics;

namespace Day24Problem2
{
    public class Hailstone
    {
        public long x0;
        public long y0;
        public long z0;

        public int vx;
        public int vy;
        public int vz;
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            var stones = new List<Hailstone>();
            foreach (string line in inputLines)
            {
                var halves = line.Split('@');
                Debug.Assert(halves.Length == 2);
                var coords = halves[0].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Debug.Assert(coords.Length == 3);
                var velocities = halves[1].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Debug.Assert(velocities.Length == 3);
                var stone = new Hailstone
                {
                    x0 = long.Parse(coords[0]),
                    y0 = long.Parse(coords[1]),
                    z0 = long.Parse(coords[2]),
                    vx = int.Parse(velocities[0]),
                    vy = int.Parse(velocities[1]),
                    vz = int.Parse(velocities[2])
                };
                stones.Add(stone);
            }

            using (var ctx = new Context())
            {
                var solver = ctx.MkSolver();
                ArithExpr magic_px = (ArithExpr)ctx.MkConst(ctx.MkSymbol(nameof(magic_px)), ctx.MkIntSort());
                ArithExpr magic_py = (ArithExpr)ctx.MkConst(ctx.MkSymbol(nameof(magic_py)), ctx.MkIntSort());
                ArithExpr magic_pz = (ArithExpr)ctx.MkConst(ctx.MkSymbol(nameof(magic_pz)), ctx.MkIntSort());
                ArithExpr magic_vx = (ArithExpr)ctx.MkConst(ctx.MkSymbol(nameof(magic_vx)), ctx.MkIntSort());
                ArithExpr magic_vy = (ArithExpr)ctx.MkConst(ctx.MkSymbol(nameof(magic_vy)), ctx.MkIntSort());
                ArithExpr magic_vz = (ArithExpr)ctx.MkConst(ctx.MkSymbol(nameof(magic_vz)), ctx.MkIntSort());
                for (int i = 0; i < stones.Count; i++)
                {
                    Hailstone stone = stones[i];
                    ArithExpr stone_px = ctx.MkInt(stone.x0);
                    ArithExpr stone_py = ctx.MkInt(stone.y0);
                    ArithExpr stone_pz = ctx.MkInt(stone.z0);
                    ArithExpr stone_vx = ctx.MkInt(stone.vx);
                    ArithExpr stone_vy = ctx.MkInt(stone.vy);
                    ArithExpr stone_vz = ctx.MkInt(stone.vz);
                    ArithExpr t = (ArithExpr)ctx.MkConst(ctx.MkSymbol($"{nameof(t)}{i}"), ctx.MkIntSort());
                    solver.Assert(ctx.MkGt(t, ctx.MkInt(0)));

                    ArithExpr lhs_x = ctx.MkAdd(ctx.MkMul(magic_vx, t), magic_px);
                    ArithExpr rhs_x = ctx.MkAdd(ctx.MkMul(stone_vx, t), stone_px);
                    solver.Assert(ctx.MkEq(lhs_x, rhs_x));

                    ArithExpr lhs_y = ctx.MkAdd(ctx.MkMul(magic_vy, t), magic_py);
                    ArithExpr rhs_y = ctx.MkAdd(ctx.MkMul(stone_vy, t), stone_py);
                    solver.Assert(ctx.MkEq(lhs_y, rhs_y));

                    ArithExpr lhs_z = ctx.MkAdd(ctx.MkMul(magic_vz, t), magic_pz);
                    ArithExpr rhs_z = ctx.MkAdd(ctx.MkMul(stone_vz, t), stone_pz);
                    solver.Assert(ctx.MkEq(lhs_z, rhs_z));
                }

                solver.Check();

                foreach (var solution in solver.Model.Consts.Where(c => c.Key.Name.ToString().StartsWith("magic_")))
                {
                    Console.Out.WriteLine($"{solution.Key.Name}: {solution.Value}");
                }
            }
        }
    }
}
