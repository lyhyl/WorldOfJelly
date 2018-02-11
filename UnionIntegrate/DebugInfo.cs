using System.Collections.Generic;

namespace UnionIntegrate
{
    public class DebugInfo
    {
        private List<List<JellyVertex>> vertex_frame = new List<List<JellyVertex>>();

        private DebugInfo()
        {
        }

        public static DebugInfo Create(UnionIntegrate world)
        {
            DebugInfo dbginfo = new DebugInfo();

            foreach (JellyObject jo in world.jobjlist)
            {
                List<JellyVertex> jvlist = new List<JellyVertex>();
                foreach (JellyVertex jv in jo.Edge)
                    jvlist.Add(new JellyVertex(jv));
                dbginfo.vertex_frame.Add(jvlist);
            }

            return dbginfo;
        }

        public string ToString(int id)
        {
            string res = "Frame " + id + " :\n<\n";

            foreach (List<JellyVertex> jvs in vertex_frame)
            {
                res += "\tJellyObject\n\t<\n";
                foreach (JellyVertex jv in jvs)
                {
                    res += "\t\t<";
                    //res += jv.Position.ToString() + "," + jv.Velocity.ToString();
                    res += jv.GetForce() + "," + jv.Velocity.ToString();
                    res += ">\n";
                }
                res += "\t>\n";
            }
            res += ">\n";
            return res;
        }
    }
}
