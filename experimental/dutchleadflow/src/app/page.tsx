"use client";

import Link from "next/link";
import { useState } from "react";

const painPoints = [
  "Leads handmatig zoeken kost uren per week.",
  "Outreach stopt zodra de agenda vol loopt.",
  "Er is geen vaste flow voor opvolging en deduplicatie.",
  "WhatsApp outreach voelt rommelig, riskant en slecht meetbaar.",
];

const features = [
  {
    title: "Targeted lead sourcing",
    description:
      "We filteren bedrijven op niche, locatie en kanaal, zodat outreach niet op willekeur draait.",
  },
  {
    title: "Smart message flow",
    description:
      "Berichten worden logisch opgebouwd met cooldowns, varianten en duidelijke CTA’s.",
  },
  {
    title: "No duplicate chaos",
    description:
      "Bestaande contacten, eerdere gesprekken en dubbele records worden automatisch uitgefilterd.",
  },
  {
    title: "Launch-ready in days",
    description:
      "Geen maandenlange implementatie. De eerste versie staat in dagen, met snelle iteraties daarna.",
  },
];

const process = [
  "Korte intake over doelgroep, aanbod en tone of voice.",
  "Leadcriteria, filters en outreachregels instellen.",
  "Automatisering koppelen en eerste flow live zetten.",
  "Optimaliseren op replies, afspraken en conversie.",
];

const faqs = [
  {
    q: "Voor wie is DutchLeadFlow bedoeld?",
    a: "Voor lokale dienstverleners, agencies, recruiters en kleine teams die direct meer afspraken uit outbound willen halen zonder alles handmatig te doen.",
  },
  {
    q: "Is dit een SaaS of een service?",
    a: "In de eerste fase is het een productized service met een sterke softwarelaag: snelle setup, vaste deliverables en iteraties op basis van resultaat.",
  },
  {
    q: "Hoe snel kan dit live?",
    a: "Een eerste versie kan vaak binnen 48 uur staan, afhankelijk van niche, data en gewenste flow.",
  },
  {
    q: "Kan ik direct betalen?",
    a: "De codebase is payment-ready opgezet. Zodra Stripe-sleutels zijn ingevuld kan de checkout-button direct live.",
  },
];

function SectionLabel({ children }: { children: React.ReactNode }) {
  return (
    <div className="inline-flex items-center gap-2 rounded-full border border-white/15 bg-white/8 px-4 py-2 text-xs font-semibold uppercase tracking-[0.24em] text-cyan-100/80 backdrop-blur-xl">
      <span className="h-2 w-2 rounded-full bg-cyan-300 shadow-[0_0_18px_rgba(103,232,249,0.8)]" />
      {children}
    </div>
  );
}

function Card({ title, description }: { title: string; description: string }) {
  return (
    <div className="group rounded-[2rem] border border-white/10 bg-white/[0.06] p-6 backdrop-blur-2xl transition duration-500 hover:-translate-y-1 hover:border-cyan-300/30 hover:bg-white/[0.09]">
      <div className="mb-4 inline-flex rounded-full border border-cyan-300/20 bg-cyan-300/10 px-3 py-1 text-[11px] font-semibold uppercase tracking-[0.22em] text-cyan-100/80">
        Feature
      </div>
      <h3 className="text-2xl font-semibold tracking-tight text-white">{title}</h3>
      <p className="mt-3 text-sm leading-7 text-white/70">{description}</p>
    </div>
  );
}

const initialForm = { name: "", email: "", niche: "", goal: "" };

export default function Home() {
  const [form, setForm] = useState(initialForm);
  const [state, setState] = useState<"idle" | "saving" | "saved" | "error">("idle");
  const [message, setMessage] = useState("");

  async function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    setState("saving");
    setMessage("");

    try {
      const res = await fetch("/api/leads", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(form),
      });

      const data = (await res.json()) as { message?: string };

      if (!res.ok) throw new Error(data.message || "Er ging iets mis.");

      setState("saved");
      setMessage(data.message || "Lead opgeslagen.");
      setForm(initialForm);
    } catch (error) {
      setState("error");
      setMessage(error instanceof Error ? error.message : "Opslaan mislukt.");
    }
  }

  return (
    <main className="relative overflow-hidden bg-[#07111f] text-white">
      <div className="pointer-events-none absolute inset-0">
        <div className="absolute left-1/2 top-0 h-[32rem] w-[32rem] -translate-x-1/2 rounded-full bg-cyan-500/20 blur-[140px]" />
        <div className="absolute right-[-10rem] top-40 h-[28rem] w-[28rem] rounded-full bg-fuchsia-500/15 blur-[150px]" />
        <div className="absolute left-[-10rem] bottom-0 h-[24rem] w-[24rem] rounded-full bg-blue-600/20 blur-[120px]" />
        <div className="absolute inset-0 bg-[linear-gradient(rgba(255,255,255,0.04)_1px,transparent_1px),linear-gradient(90deg,rgba(255,255,255,0.04)_1px,transparent_1px)] bg-[size:72px_72px] [mask-image:radial-gradient(circle_at_center,black,transparent_82%)]" />
      </div>

      <section className="relative mx-auto flex min-h-screen w-full max-w-7xl flex-col px-6 pb-20 pt-6 sm:px-10 lg:px-12">
        <header className="mb-16 flex items-center justify-between rounded-full border border-white/10 bg-white/6 px-5 py-4 backdrop-blur-2xl">
          <div>
            <div className="text-sm font-semibold tracking-[0.35em] text-cyan-200 uppercase">
              DutchLeadFlow
            </div>
            <div className="text-xs text-white/55">WhatsApp outreach engine for local growth</div>
          </div>
          <div className="flex items-center gap-3">
            <Link
              href="/dashboard"
              className="rounded-full border border-white/12 bg-white/[0.05] px-4 py-2 text-sm font-medium text-white/80 transition hover:border-white/25 hover:bg-white/[0.08]"
            >
              Dashboard preview
            </Link>
            <a
              href="#cta"
              className="rounded-full border border-cyan-300/35 bg-cyan-300/10 px-4 py-2 text-sm font-medium text-cyan-100 transition duration-300 hover:border-cyan-200 hover:bg-cyan-300/20"
            >
              Book launch slot
            </a>
          </div>
        </header>

        <div className="grid flex-1 items-center gap-12 lg:grid-cols-[1.1fr_0.9fr]">
          <div>
            <SectionLabel>Antigravity launch page</SectionLabel>
            <h1 className="mt-8 max-w-4xl text-5xl font-semibold leading-[1.02] tracking-tight sm:text-6xl xl:text-7xl">
              Turn WhatsApp outreach into a repeatable growth system.
            </h1>
            <p className="mt-8 max-w-2xl text-lg leading-8 text-white/72 sm:text-xl">
              DutchLeadFlow helps local businesses launch a high-converting WhatsApp outreach setup with filtered leads,
              clean automations and fast iteration — without drowning in manual follow-up.
            </p>

            <div className="mt-10 flex flex-col gap-4 sm:flex-row">
              <a
                href="#cta"
                className="inline-flex items-center justify-center rounded-full bg-white px-7 py-4 text-sm font-semibold text-slate-950 shadow-[0_22px_50px_rgba(255,255,255,0.18)] transition duration-300 hover:-translate-y-0.5 hover:shadow-[0_30px_70px_rgba(255,255,255,0.22)]"
              >
                Claim a launch slot
              </a>
              <a
                href="#offer"
                className="inline-flex items-center justify-center rounded-full border border-white/14 bg-white/[0.05] px-7 py-4 text-sm font-semibold text-white/90 backdrop-blur-xl transition duration-300 hover:border-cyan-300/35 hover:bg-white/[0.09]"
              >
                Explore the offer
              </a>
            </div>

            <div className="mt-12 grid gap-4 sm:grid-cols-3">
              {[
                ["48h", "First launch-ready setup"],
                ["€199", "Launch offer starter setup"],
                ["1 flow", "One focused sales machine"],
              ].map(([value, label]) => (
                <div
                  key={label}
                  className="rounded-[1.75rem] border border-white/10 bg-white/[0.05] px-5 py-5 backdrop-blur-2xl"
                >
                  <div className="text-3xl font-semibold tracking-tight text-white">{value}</div>
                  <div className="mt-1 text-sm text-white/60">{label}</div>
                </div>
              ))}
            </div>
          </div>

          <div className="relative" style={{ perspective: "1400px" }}>
            <div className="absolute inset-0 translate-y-8 rounded-[2.5rem] bg-cyan-400/20 blur-3xl" />
            <div className="relative overflow-hidden rounded-[2.5rem] border border-white/12 bg-white/[0.07] p-6 backdrop-blur-3xl shadow-[0_40px_120px_rgba(0,0,0,0.35)] transition duration-500 hover:-translate-y-1">
              <div className="flex items-center justify-between rounded-full border border-white/10 bg-slate-950/35 px-4 py-3 text-xs uppercase tracking-[0.22em] text-white/55">
                <span>Revenue engine</span>
                <span className="rounded-full bg-emerald-400/15 px-3 py-1 text-emerald-200">Launch-ready</span>
              </div>

              <div className="mt-6 space-y-4">
                {painPoints.map((point, index) => (
                  <div
                    key={point}
                    className="flex items-start gap-4 rounded-[1.4rem] border border-white/10 bg-black/20 px-4 py-4"
                  >
                    <div className="flex h-10 w-10 shrink-0 items-center justify-center rounded-2xl bg-cyan-300/12 text-sm font-semibold text-cyan-100">
                      0{index + 1}
                    </div>
                    <p className="text-sm leading-7 text-white/72">{point}</p>
                  </div>
                ))}
              </div>

              <div className="mt-6 rounded-[1.7rem] border border-cyan-300/18 bg-gradient-to-br from-cyan-300/14 to-fuchsia-400/10 p-5">
                <div className="text-xs font-semibold uppercase tracking-[0.24em] text-cyan-100/80">Outcome</div>
                <div className="mt-3 text-2xl font-semibold tracking-tight text-white">
                  More replies. Less chaos. A system your business can actually run.
                </div>
                <p className="mt-3 text-sm leading-7 text-white/70">
                  Built for speed, clarity and conversion — with clean automation rules and a modern handoff process.
                </p>
              </div>
            </div>
          </div>
        </div>
      </section>

      <section id="offer" className="relative mx-auto max-w-7xl px-6 pb-24 sm:px-10 lg:px-12">
        <div className="mb-10 flex flex-col gap-5 md:max-w-3xl">
          <SectionLabel>Offer</SectionLabel>
          <h2 className="text-4xl font-semibold tracking-tight sm:text-5xl">A focused launch offer, not another bloated agency package.</h2>
          <p className="text-lg leading-8 text-white/68">
            The first version is deliberately sharp: one audience, one flow, one measurable outbound channel.
            That keeps launch velocity high and decision-making clean.
          </p>
        </div>

        <div className="grid gap-5 md:grid-cols-2 xl:grid-cols-4">
          {features.map((feature) => (
            <Card key={feature.title} title={feature.title} description={feature.description} />
          ))}
        </div>
      </section>

      <section className="relative mx-auto max-w-7xl px-6 pb-24 sm:px-10 lg:px-12">
        <div className="grid gap-8 lg:grid-cols-[0.85fr_1.15fr]">
          <div className="rounded-[2rem] border border-white/10 bg-white/[0.05] p-8 backdrop-blur-3xl">
            <SectionLabel>Launch process</SectionLabel>
            <h2 className="mt-6 text-3xl font-semibold tracking-tight sm:text-4xl">Built to move in days, not drag for weeks.</h2>
            <p className="mt-4 text-base leading-8 text-white/70">
              The offer is intentionally productized: clear scope, clear handoff, clear outcome.
            </p>
          </div>

          <div className="grid gap-4">
            {process.map((step, index) => (
              <div
                key={step}
                className="flex items-start gap-5 rounded-[1.8rem] border border-white/10 bg-white/[0.05] px-6 py-6 backdrop-blur-3xl transition duration-300 hover:border-fuchsia-300/20 hover:bg-white/[0.075]"
              >
                <div className="flex h-12 w-12 shrink-0 items-center justify-center rounded-2xl bg-gradient-to-br from-cyan-300/25 to-fuchsia-400/25 text-sm font-semibold text-white">
                  {index + 1}
                </div>
                <p className="text-base leading-8 text-white/72">{step}</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      <section className="relative mx-auto max-w-7xl px-6 pb-24 sm:px-10 lg:px-12">
        <div className="grid gap-8 lg:grid-cols-[1fr_0.9fr]">
          <div className="rounded-[2.2rem] border border-cyan-300/15 bg-gradient-to-br from-white/[0.08] to-white/[0.03] p-8 backdrop-blur-3xl">
            <SectionLabel>Pricing</SectionLabel>
            <h2 className="mt-6 text-4xl font-semibold tracking-tight">Simple launch pricing to validate fast.</h2>
            <div className="mt-8 grid gap-4 md:grid-cols-2">
              <div className="rounded-[1.75rem] border border-white/10 bg-slate-950/35 p-6">
                <div className="text-sm uppercase tracking-[0.24em] text-white/45">Starter setup</div>
                <div className="mt-4 text-5xl font-semibold tracking-tight">€199</div>
                <p className="mt-4 text-sm leading-7 text-white/68">
                  One-time launch package for a first working setup, intake, copy direction and flow configuration.
                </p>
                <div className="mt-5 text-xs text-white/45">Wire this to Stripe checkout in /api/checkout</div>
              </div>
              <div className="rounded-[1.75rem] border border-cyan-300/20 bg-cyan-300/10 p-6">
                <div className="text-sm uppercase tracking-[0.24em] text-cyan-100/80">Optimization</div>
                <div className="mt-4 text-5xl font-semibold tracking-tight">€99</div>
                <p className="mt-4 text-sm leading-7 text-white/74">
                  Monthly iteration layer for message testing, performance tuning and flow refinement.
                </p>
              </div>
            </div>
          </div>

          <div className="space-y-4 rounded-[2.2rem] border border-white/10 bg-white/[0.05] p-8 backdrop-blur-3xl">
            <SectionLabel>FAQ</SectionLabel>
            {faqs.map((item) => (
              <div key={item.q} className="rounded-[1.5rem] border border-white/8 bg-black/20 p-5">
                <h3 className="text-lg font-semibold text-white">{item.q}</h3>
                <p className="mt-2 text-sm leading-7 text-white/68">{item.a}</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      <section id="cta" className="relative mx-auto max-w-7xl px-6 pb-28 sm:px-10 lg:px-12">
        <div className="overflow-hidden rounded-[2.4rem] border border-white/10 bg-white/[0.06] p-8 backdrop-blur-3xl sm:p-10">
          <div className="grid gap-10 lg:grid-cols-[1fr_0.88fr] lg:items-center">
            <div>
              <SectionLabel>CTA</SectionLabel>
              <h2 className="mt-6 text-4xl font-semibold tracking-tight sm:text-5xl">
                Want the first launch slot before this gets crowded?
              </h2>
              <p className="mt-5 max-w-2xl text-lg leading-8 text-white/70">
                This version already captures leads, stores them locally and is structured so a payment step can be wired in next.
              </p>
            </div>

            <form onSubmit={handleSubmit} className="rounded-[2rem] border border-white/10 bg-slate-950/35 p-6">
              <div className="space-y-4">
                <input
                  type="text"
                  required
                  value={form.name}
                  onChange={(e) => setForm((f) => ({ ...f, name: e.target.value }))}
                  placeholder="Naam"
                  className="w-full rounded-2xl border border-white/10 bg-white/[0.06] px-4 py-4 text-sm text-white outline-none placeholder:text-white/35 focus:border-cyan-300/40"
                />
                <input
                  type="email"
                  required
                  value={form.email}
                  onChange={(e) => setForm((f) => ({ ...f, email: e.target.value }))}
                  placeholder="E-mail"
                  className="w-full rounded-2xl border border-white/10 bg-white/[0.06] px-4 py-4 text-sm text-white outline-none placeholder:text-white/35 focus:border-cyan-300/40"
                />
                <input
                  type="text"
                  required
                  value={form.niche}
                  onChange={(e) => setForm((f) => ({ ...f, niche: e.target.value }))}
                  placeholder="Bedrijf of niche"
                  className="w-full rounded-2xl border border-white/10 bg-white/[0.06] px-4 py-4 text-sm text-white outline-none placeholder:text-white/35 focus:border-cyan-300/40"
                />
                <textarea
                  required
                  value={form.goal}
                  onChange={(e) => setForm((f) => ({ ...f, goal: e.target.value }))}
                  placeholder="Waar wil je outreach voor inzetten?"
                  rows={4}
                  className="w-full rounded-[1.4rem] border border-white/10 bg-white/[0.06] px-4 py-4 text-sm text-white outline-none placeholder:text-white/35 focus:border-cyan-300/40"
                />
                <button
                  disabled={state === "saving"}
                  className="w-full rounded-2xl bg-white px-5 py-4 text-sm font-semibold text-slate-950 transition duration-300 hover:-translate-y-0.5 hover:bg-cyan-100 disabled:cursor-not-allowed disabled:opacity-70"
                >
                  {state === "saving" ? "Saving..." : "Request launch call"}
                </button>
                <a
                  href="/api/checkout?plan=starter"
                  className="block w-full rounded-2xl border border-cyan-300/30 bg-cyan-300/10 px-5 py-4 text-center text-sm font-semibold text-cyan-100 transition hover:bg-cyan-300/15"
                >
                  Preview starter checkout
                </a>
                {message ? (
                  <p className={`text-xs leading-6 ${state === "error" ? "text-rose-300" : "text-emerald-300"}`}>
                    {message}
                  </p>
                ) : null}
              </div>
            </form>
          </div>
        </div>
      </section>
    </main>
  );
}
