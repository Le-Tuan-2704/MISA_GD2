-- phpMyAdmin SQL Dump
-- version 5.1.0
-- https://www.phpmyadmin.net/
--
-- Máy chủ: 127.0.0.1
-- Thời gian đã tạo: Th12 24, 2021 lúc 02:42 PM
-- Phiên bản máy phục vụ: 10.4.18-MariaDB
-- Phiên bản PHP: 7.4.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `work_scheduling`
--

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `eventcalendar`
--

CREATE TABLE `eventcalendar` (
  `eventcalendarId` char(36) NOT NULL DEFAULT '',
  `title` varchar(100) DEFAULT NULL,
  `content` varchar(255) DEFAULT NULL,
  `start` datetime DEFAULT NULL,
  `end` datetime DEFAULT NULL,
  `employeeId` char(36) DEFAULT NULL,
  `approverId` char(36) DEFAULT NULL,
  `groupId` char(36) DEFAULT NULL,
  `currentStatus` tinyint(4) DEFAULT NULL,
  `createdDate` datetime DEFAULT current_timestamp(),
  `updatedDate` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Đang đổ dữ liệu cho bảng `eventcalendar`
--

INSERT INTO `eventcalendar` (`eventcalendarId`, `title`, `content`, `start`, `end`, `employeeId`, `approverId`, `groupId`, `currentStatus`, `createdDate`, `updatedDate`) VALUES
('1e77c7db-6499-11ec-b491-0a0027000007', 'hi', 'xin', '2021-12-24 16:08:54', '2021-12-24 16:08:54', 'e47bbd8d-6499-11ec-b491-0a0027000007', '18069010-649a-11ec-b491-0a0027000007', '1d141d2b-649a-11ec-b491-0a0027000007', 0, '2021-12-24 16:08:54', '2021-12-24 16:08:54'),
('57f25763-6499-11ec-b491-0a0027000007', 'xin nghỉ phép', 'do ốm', '2021-12-24 16:10:30', '2021-12-24 16:10:30', 'e47bdf16-6499-11ec-b491-0a0027000007', '1806db5d-649a-11ec-b491-0a0027000007', '1d14352b-649a-11ec-b491-0a0027000007', 0, '2021-12-24 16:10:30', '2021-12-24 16:10:30'),
('bce4301e-6498-11ec-b491-0a0027000007', 'hello', 'xin chào', '2021-12-24 16:06:10', '2021-12-24 16:06:10', 'e47bdf79-6499-11ec-b491-0a0027000007', '1806dbd1-649a-11ec-b491-0a0027000007', '1d143591-649a-11ec-b491-0a0027000007', 0, '2021-12-24 16:06:10', '2021-12-24 16:06:10');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tokens`
--

CREATE TABLE `tokens` (
  `idToken` char(36) NOT NULL DEFAULT '',
  `token` varchar(100) DEFAULT NULL,
  `idUser` char(36) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Đang đổ dữ liệu cho bảng `tokens`
--

INSERT INTO `tokens` (`idToken`, `token`, `idUser`) VALUES
('1dfa1988-f889-43d3-86e1-2e8f944e0402', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJBIiwianRpIjoiMGUxZDgzYjctMzdlYi00ZDA1LWE1N2UtNjAwN2V', 'fea5c664-63c6-11ec-ab6f-0a0027000007'),
('65c0554f-9191-4655-8196-6b0e8e7d7220', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJiIiwianRpIjoiMzI3OTgzMmUtNzQ2My00NGQzLWIyNjItMGNhZjM', '8d33007a-63ca-11ec-ab6f-0a0027000007'),
('753d8054-79a9-43bc-8e63-90a498a73498', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwianRpIjoiOGQzMzAwN2EtNjNjYS0xMWVjLWFiNmYtMGEwMDI', '8d33007a-63ca-11ec-ab6f-0a0027000007');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `users`
--

CREATE TABLE `users` (
  `idUser` char(36) NOT NULL DEFAULT '',
  `userName` varchar(100) NOT NULL DEFAULT '',
  `password` varchar(60) NOT NULL DEFAULT '',
  `role` tinyint(4) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Đang đổ dữ liệu cho bảng `users`
--

INSERT INTO `users` (`idUser`, `userName`, `password`, `role`) VALUES
('8d33007a-63ca-11ec-ab6f-0a0027000007', 'b', '$2a$11$S5AzxK.9/qeUFcfi1Zv1qOS1.GqRCviyQTF/JdDPNtF4N45UdRtPm', 1),
('fea5c664-63c6-11ec-ab6f-0a0027000007', 'a', '$2a$11$VXQGry1/BfQwvKNJCsMnyeZBm2xzZf18IqYMvaRF9.VSue.cBYrn6', 1);

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `eventcalendar`
--
ALTER TABLE `eventcalendar`
  ADD PRIMARY KEY (`eventcalendarId`);

--
-- Chỉ mục cho bảng `tokens`
--
ALTER TABLE `tokens`
  ADD PRIMARY KEY (`idToken`),
  ADD KEY `FK_tokens_idUser` (`idUser`);

--
-- Chỉ mục cho bảng `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`idUser`);

--
-- Các ràng buộc cho các bảng đã đổ
--

--
-- Các ràng buộc cho bảng `tokens`
--
ALTER TABLE `tokens`
  ADD CONSTRAINT `FK_tokens_idUser` FOREIGN KEY (`idUser`) REFERENCES `users` (`idUser`) ON DELETE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
