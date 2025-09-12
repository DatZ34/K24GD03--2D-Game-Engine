# BattleGrid

## 🎮 Thể loại
Game chiến thuật **Turn-Based** kết hợp **thẻ bài (Card System)** trên bàn cờ 5x5.

## 🕹️ Cách chơi
- Người chơi và kẻ địch được spawn ở hai góc đối diện bản đồ.  
- Mỗi lượt, người chơi chọn 1 ô hợp lệ để di chuyển.  
- Khi đứng cạnh kẻ địch, người chơi có thể tấn công trực tiếp.  
- Ngoài di chuyển cơ bản, người chơi còn có thể **sử dụng các loại thẻ (Card)**:
  - **Attack Card**: Tăng sát thương cho lượt tấn công.  
  - **Defend Card**: Tạo giáp để giảm sát thương nhận vào.  
  - **Skill Card**: Gây hiệu ứng đặc biệt (đẩy lùi, choáng, hồi máu...).  
- Kẻ địch sẽ tự động tìm đường (pathfinding) để tiếp cận và tấn công người chơi.  

## 🎯 Mục tiêu
- Xây dựng gameplay chiến thuật ngắn gọn, dễ hiểu nhưng có chiều sâu nhờ hệ thống thẻ.  
- Hoàn thiện một bản **prototype có thể chơi được** để kiểm thử cơ chế core loop:  
  > Spawn → Di chuyển → Sử dụng thẻ → Tấn công → Kết thúc lượt.  

## 📌 Tiến độ hiện tại
- ✅ Spawn Player và Enemy theo tilemap.  
- ✅ Di chuyển theo lượt trên grid.  
- ✅ Enemy pathfinding & tấn công cơ bản.  
- ⚙️ Đang phát triển: Hệ thống thẻ (card) và hiệu ứng chiến đấu.  
- ⏳ Sắp tới: UI quản lý thẻ, nhiều loại Enemy khác nhau.  

## 📷 Screenshot
- ![Gameplay](c:\Users\AD\Downloads\gameplay.jpg)  
- ![Pause](c:\Users\AD\Downloads\pause.jpg)  
- ![MainMenu](c:\Users\AD\Downloads\mainMenu.jpg)  
- ![Win](c:\Users\AD\Downloads\win.jpg)  
- ![Lose](c:\Users\AD\Downloads\lose.jpg)  
