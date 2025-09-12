<img width="1753" height="811" alt="image" src="https://github.com/user-attachments/assets/c9dfa745-22d3-4737-8814-108cf03d0756" /><img width="1753" height="811" alt="image" src="https://github.com/user-attachments/assets/9f638499-5d40-4f23-a704-54fa21468b24" /># BattleGrid

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

- ![Gameplay] <img width="1753" height="811" alt="image" src="https://github.com/user-attachments/assets/1bbaf228-a60c-4060-bdf8-601db93916ef" />
- ![Pause]<img width="1042" height="765" alt="image" src="https://github.com/user-attachments/assets/fe757f7a-f247-4dd0-be78-917648af0ce1" />
- ![MainMenu]<img width="1049" height="767" alt="image" src="https://github.com/user-attachments/assets/61719309-c2f1-4b42-b9ae-4f3f4401a509" />
- ![Win]<img width="1046" height="776" alt="image" src="https://github.com/user-attachments/assets/95b8bd11-d19a-469b-981e-cb865e14eb7c" />
- ![Lose]<img width="1045" height="769" alt="image" src="https://github.com/user-attachments/assets/a2c359e5-c979-4905-9a68-cd6980822f13" />

