using Microsoft.Z3;

namespace Day24Problem2
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            var stones = new List<Hailstone>();
            foreach (string line in inputLines)
            {
                var halves = line.Split('@');
                if (halves.Length != 2) throw new ArgumentException("Expected input to contain both positions and velocities.");
                var coords = halves[0].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (coords.Length != 3) throw new ArgumentException("Expected x, y, and z components in each input position.");
                var velocities = halves[1].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (velocities.Length != 3) throw new ArgumentException("Expected vx, vy, and vz components in each input velocity.");
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
                HashSet<string> initialPositionVariableNames = [nameof(magic_px), nameof(magic_py), nameof(magic_pz)];
                return solver.Model.Consts
                    .Where(c => initialPositionVariableNames.Contains(((StringSymbol)c.Key.Name).String))
                    .Select(p => ((IntNum)p.Value).Int64)
                    .Sum();
            }
        }
    }
}
