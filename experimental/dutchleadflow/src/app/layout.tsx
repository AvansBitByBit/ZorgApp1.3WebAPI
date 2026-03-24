import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "DutchLeadFlow — WhatsApp outreach engine",
  description:
    "A premium launch page for a WhatsApp outreach automation offer focused on local business growth.",
  metadataBase: new URL("https://example.com"),
  openGraph: {
    title: "DutchLeadFlow",
    description: "Launch a premium WhatsApp outreach system for local business growth.",
    url: "/",
    siteName: "DutchLeadFlow",
    type: "website",
  },
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html
      lang="en"
      className={`${geistSans.variable} ${geistMono.variable} h-full antialiased`}
    >
      <body className="min-h-full flex flex-col">{children}</body>
    </html>
  );
}
