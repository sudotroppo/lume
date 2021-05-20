/*
  Warnings:

  - You are about to drop the column `name` on the `User` table. All the data in the column will be lost.
  - You are about to drop the column `lastName` on the `User` table. All the data in the column will be lost.
  - A unique constraint covering the columns `[email]` on the table `User` will be added. If there are existing duplicate values, this will fail.
  - Added the required column `nome` to the `User` table without a default value. This is not possible if the table is not empty.
  - Added the required column `cognome` to the `User` table without a default value. This is not possible if the table is not empty.

*/
-- DropIndex
DROP INDEX "User.id_unique";

-- AlterTable
ALTER TABLE "User" DROP COLUMN "name",
DROP COLUMN "lastName",
ADD COLUMN     "nome" TEXT NOT NULL,
ADD COLUMN     "cognome" TEXT NOT NULL,
ADD PRIMARY KEY ("id");

-- CreateIndex
CREATE UNIQUE INDEX "User.email_unique" ON "User"("email");
