import { mkdir, readFile, writeFile } from "node:fs/promises";
import path from "node:path";
import { NextResponse } from "next/server";

const dataDir = path.join(process.cwd(), "data");
const leadsPath = path.join(dataDir, "leads.json");

type Lead = {
  id: string;
  name: string;
  email: string;
  niche: string;
  goal: string;
  createdAt: string;
};

async function readLeads(): Promise<Lead[]> {
  try {
    const raw = await readFile(leadsPath, "utf8");
    return JSON.parse(raw) as Lead[];
  } catch {
    return [];
  }
}

export async function POST(req: Request) {
  const body = (await req.json()) as Partial<Lead>;

  if (!body.name || !body.email || !body.niche || !body.goal) {
    return NextResponse.json({ message: "Missing required fields." }, { status: 400 });
  }

  const lead: Lead = {
    id: crypto.randomUUID(),
    name: body.name.trim(),
    email: body.email.trim(),
    niche: body.niche.trim(),
    goal: body.goal.trim(),
    createdAt: new Date().toISOString(),
  };

  await mkdir(dataDir, { recursive: true });
  const leads = await readLeads();
  leads.unshift(lead);
  await writeFile(leadsPath, JSON.stringify(leads, null, 2), "utf8");

  return NextResponse.json({ message: "Lead saved successfully.", lead });
}

export async function GET() {
  const leads = await readLeads();
  return NextResponse.json({ count: leads.length, leads });
}
