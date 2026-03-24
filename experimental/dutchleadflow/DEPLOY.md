# Deploy DutchLeadFlow

## Option 1: Vercel

1. Push this folder to GitHub.
2. Import the repo in Vercel.
3. Set these environment variables:
   - `NEXT_PUBLIC_SITE_URL`
   - `STRIPE_SECRET_KEY` (optional for now)
4. Deploy.

## Option 2: Local Node host

```bash
npm install
npm run build
npm run start
```

Default runtime port can be set with:

```bash
PORT=3000 npm run start
```

## What already works
- premium landing page
- local lead capture via `/api/leads`
- internal dashboard preview at `/dashboard`
- payment-ready API placeholder at `/api/checkout?plan=starter`

## What to wire next
- real Stripe checkout session creation
- real email / CRM webhook delivery
- analytics
- domain + HTTPS
