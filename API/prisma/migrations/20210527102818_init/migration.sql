/*
  Warnings:

  - You are about to drop the `User` table. If the table is not empty, all the data it contains will be lost.

*/
-- DropTable
DROP TABLE "User";

-- CreateTable
CREATE TABLE "utente" (
    "id" SERIAL NOT NULL,
    "email" TEXT NOT NULL,
    "nome" TEXT NOT NULL,
    "cognome" TEXT NOT NULL,
    "telefono" TEXT NOT NULL,

    PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "richiesta" (
    "id" SERIAL NOT NULL,
    "titolo" TEXT NOT NULL,
    "descrizione" TEXT NOT NULL,
    "nPersone" INTEGER NOT NULL,
    "utente_id" INTEGER NOT NULL,

    PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "notifica" (
    "id" SERIAL NOT NULL,
    "titolo" TEXT NOT NULL,
    "descrizione" TEXT NOT NULL,
    "utente_id" INTEGER NOT NULL,
    "richiesta_id" INTEGER NOT NULL,

    PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "utenteOnRichiesta" (
    "richiesta_id" INTEGER NOT NULL,
    "utente_id" INTEGER NOT NULL,
    "createdAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY ("richiesta_id","utente_id")
);

-- CreateIndex
CREATE UNIQUE INDEX "utente.email_unique" ON "utente"("email");

-- AddForeignKey
ALTER TABLE "richiesta" ADD FOREIGN KEY ("utente_id") REFERENCES "utente"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "notifica" ADD FOREIGN KEY ("utente_id") REFERENCES "utente"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "notifica" ADD FOREIGN KEY ("richiesta_id") REFERENCES "richiesta"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "utenteOnRichiesta" ADD FOREIGN KEY ("richiesta_id") REFERENCES "richiesta"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "utenteOnRichiesta" ADD FOREIGN KEY ("utente_id") REFERENCES "utente"("id") ON DELETE CASCADE ON UPDATE CASCADE;
