import { NextResponse } from "next/server";

const pricing = {
  starter: { name: "Starter Setup", amount: 199 },
  optimize: { name: "Monthly Optimization", amount: 99 },
} as const;

export async function GET(req: Request) {
  const { searchParams } = new URL(req.url);
  const plan = searchParams.get("plan") as keyof typeof pricing | null;

  if (!plan || !pricing[plan]) {
    return NextResponse.json({ message: "Unknown plan." }, { status: 400 });
  }

  const stripeKeyPresent = Boolean(process.env.STRIPE_SECRET_KEY);

  return NextResponse.json({
    checkoutReady: stripeKeyPresent,
    provider: stripeKeyPresent ? "stripe" : "placeholder",
    plan: pricing[plan],
    nextStep: stripeKeyPresent
      ? "Implement Stripe Checkout session creation here."
      : "Add STRIPE_SECRET_KEY and wire a real checkout flow.",
  });
}
