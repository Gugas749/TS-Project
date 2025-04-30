-- Criar a base de dados
CREATE DATABASE `proj-ts`;

-- Sleceionar a base de dados
USE `proj-ts`;

-- Criar tabela de clientes
CREATE TABLE `users` (
    `IDuser` INT AUTO_INCREMENT NOT NULL,
    `email` VARCHAR(255),
    `pass` VARCHAR(255),
    PRIMARY KEY (`IDuser`)
);

-- Criar tabela das mensagens
CREATE TABLE `mensages` (
	`IDmsg` INT AUTO_INCREMENT NOT NULL,
    `IDreceiver` INT NOT NULL,
    `IDsender` INT NOT NULL,
    `message_text` TEXT,
	PRIMARY KEY (`IDmsg`),
    FOREIGN KEY (`IDreceiver`) REFERENCES `users`(`IDuser`) ON DELETE CASCADE,
    FOREIGN KEY (`IDsender`) REFERENCES `users`(`IDuser`) ON DELETE CASCADE
);