-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server Version:               10.4.11-MariaDB - mariadb.org binary distribution
-- Server Betriebssystem:        Win64
-- HeidiSQL Version:             11.1.0.6116
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Exportiere Datenbank Struktur für sve2_ws
CREATE DATABASE IF NOT EXISTS `sve2_ws` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `sve2_ws`;

-- Exportiere Struktur von Tabelle sve2_ws.labs
CREATE TABLE IF NOT EXISTS `labs` (
  `IdLab` int(11) NOT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Location` varchar(50) DEFAULT NULL,
  `Timestamp` datetime DEFAULT NULL,
  PRIMARY KEY (`IdLab`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Exportiere Struktur von Tabelle sve2_ws.lists
CREATE TABLE IF NOT EXISTS `lists` (
  `IdLab` int(11) NOT NULL,
  `IdProject` int(11) NOT NULL,
  `IdList` int(11) NOT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Timestamp` datetime DEFAULT NULL,
  PRIMARY KEY (`IdList`,`IdProject`,`IdLab`),
  KEY `FK_lists_projects` (`IdLab`,`IdProject`),
  CONSTRAINT `FK_lists_projects` FOREIGN KEY (`IdLab`, `IdProject`) REFERENCES `projects` (`IdLab`, `IdProject`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Daten Export vom Benutzer nicht ausgewählt

-- Exportiere Struktur von Tabelle sve2_ws.points
CREATE TABLE IF NOT EXISTS `points` (
  `IdLab` int(11) NOT NULL,
  `IdProject` int(11) NOT NULL,
  `IdSeries` int(11) NOT NULL,
  `IdPoint` int(11) NOT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Timestamp` datetime DEFAULT NULL,
  `IdList` int(11) DEFAULT NULL,
  PRIMARY KEY (`IdLab`,`IdProject`,`IdPoint`,`IdSeries`) USING BTREE,
  KEY `FK_points_series` (`IdLab`,`IdProject`,`IdSeries`),
  KEY `FK_points_lists` (`IdLab`,`IdProject`,`IdList`),
  CONSTRAINT `FK_points_lists` FOREIGN KEY (`IdLab`, `IdProject`, `IdList`) REFERENCES `lists` (`IdLab`, `IdProject`, `IdList`),
  CONSTRAINT `FK_points_series` FOREIGN KEY (`IdLab`, `IdProject`, `IdSeries`) REFERENCES `series` (`IdLab`, `IdProject`, `IdSeries`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Exportiere Struktur von Tabelle sve2_ws.projects
CREATE TABLE IF NOT EXISTS `projects` (
  `IdLab` int(11) NOT NULL,
  `IdProject` int(11) NOT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Customer` varchar(50) DEFAULT NULL,
  `Timestamp` datetime DEFAULT NULL,
  PRIMARY KEY (`IdProject`,`IdLab`) USING BTREE,
  KEY `FK_projects_labs` (`IdLab`),
  CONSTRAINT `FK_projects_labs` FOREIGN KEY (`IdLab`) REFERENCES `labs` (`IdLab`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Exportiere Struktur von Tabelle sve2_ws.series
CREATE TABLE IF NOT EXISTS `series` (
  `IdLab` int(11) NOT NULL,
  `IdProject` int(11) NOT NULL,
  `IdSeries` int(11) NOT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Timestamp` datetime DEFAULT NULL,
  PRIMARY KEY (`IdLab`,`IdProject`,`IdSeries`),
  CONSTRAINT `FK_series_projects` FOREIGN KEY (`IdLab`, `IdProject`) REFERENCES `projects` (`IdLab`, `IdProject`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
