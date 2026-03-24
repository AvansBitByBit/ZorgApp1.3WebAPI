import Link from "next/link";
import { readFile } from "node:fs/promises";
import path from "node:path";

type Lead = {
  id: string;
  name: string;
  email: string;
  niche: string;
  goal: string;
  createdAt: string;
};

async function getLeads(): Promise<Lead[]> {
  try {
    const file = await readFile(path.join(process.cwd(), "data", "leads.json"), "utf8");
    return JSON.parse(file) as Lead[];
  } catch {
    return [];
  }
}

export default async function DashboardPage() {
  const leads = await getLeads();

  return (
    <main className="min-h-screen bg-[#06111e] px-6 py-8 text-white sm:px-10 lg:px-12">
      <div className="mx-auto max-w-7xl">
        <div className="mb-8 flex items-center justify-between rounded-[2rem] border border-white/10 bg-white/[0.05] px-6 py-5 backdrop-blur-2xl">
          <div>
            <div className="text-sm uppercase tracking-[0.3em] text-cyan-200">DutchLeadFlow</div>
            <h1 className="mt-2 text-3xl font-semibold tracking-tight">Internal dashboard preview</h1>
          </div>
          <Link
            href="/"
            className="rounded-full border border-white/12 bg-white/[0.05] px-4 py-2 text-sm text-white/80 transition hover:bg-white/[0.08]"
          >
            Back to site
          </Link>
        </div>

        <div className="grid gap-5 md:grid-cols-3">
          <div className="rounded-[1.8rem] border border-white/10 bg-white/[0.05] p-6 backdrop-blur-2xl">
            <div className="text-sm text-white/55">Leads captured</div>
            <div className="mt-3 text-5xl font-semibold tracking-tight">{leads.length}</div>
          </div>
          <div className="rounded-[1.8rem] border border-white/10 bg-white/[0.05] p-6 backdrop-blur-2xl">
            <div className="text-sm text-white/55">Checkout status</div>
            <div className="mt-3 text-2xl font-semibold tracking-tight">Payment-ready shell</div>
          </div>
          <div className="rounded-[1.8rem] border border-white/10 bg-white/[0.05] p-6 backdrop-blur-2xl">
            <div className="text-sm text-white/55">Deployment</div>
            <div className="mt-3 text-2xl font-semibold tracking-tight">Ready for Vercel / Node host</div>
          </div>
        </div>

        <div className="mt-8 rounded-[2rem] border border-white/10 bg-white/[0.05] p-6 backdrop-blur-2xl">
          <div className="mb-4 flex items-center justify-between">
            <h2 className="text-2xl font-semibold tracking-tight">Recent leads</h2>
            <span className="text-sm text-white/50">Stored locally in data/leads.json</span>
          </div>

          {leads.length === 0 ? (
            <div className="rounded-[1.5rem] border border-dashed border-white/12 px-6 py-10 text-sm text-white/55">
              No leads captured yet. Submit the homepage form to populate this dashboard.
            </div>
          ) : (
            <div className="overflow-hidden rounded-[1.5rem] border border-white/10">
              <table className="w-full border-collapse text-left text-sm">
                <thead className="bg-white/[0.04] text-white/55">
                  <tr>
                    <th className="px-4 py-3 font-medium">Name</th>
                    <th className="px-4 py-3 font-medium">Email</th>
                    <th className="px-4 py-3 font-medium">Niche</th>
                    <th className="px-4 py-3 font-medium">Goal</th>
                  </tr>
                </thead>
                <tbody>
                  {leads.map((lead) => (
                    <tr key={lead.id} className="border-t border-white/8 align-top">
                      <td className="px-4 py-4">{lead.name}</td>
                      <td className="px-4 py-4 text-white/75">{lead.email}</td>
                      <td className="px-4 py-4 text-white/75">{lead.niche}</td>
                      <td className="px-4 py-4 text-white/70">{lead.goal}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </div>
      </div>
    </main>
  );
}
