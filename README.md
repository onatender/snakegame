# C# Form Snake Game  

  C# Form ile görüntü oluşturmak için bir grid yapısı oluşturmalıydım, ben de bunu panelleri kullanıp; onları renklendirerek çözdüm.  
  ```
 Pixels = new List<Panel[]>();
            int xpay = 40;
            int ypay = 40;
            int pixel_shape_x = 20;
            int pixel_shape_y = 20;
            for (int x = 0; x < 20; x++)
            {
                Pixels.Add(new Panel[20]);
                for (int y = 0; y < 20; y++)
                {
                    Pixels[x][y] = new Panel();
                    Pixels[x][y].Size = new Size(pixel_shape_x, pixel_shape_y);
                    Pixels[x][y].BorderStyle = BorderStyle.FixedSingle;
                    Pixels[x][y].Location = new Point(y*pixel_shape_y+ypay, x*pixel_shape_x+xpay);
                    Pixels[x][y].BackColor = bgColor;
                    this.Controls.Add(Pixels[x][y]);
                }
            }
```
  
![WhatsApp Görsel 2024-01-27 saat 16 35 33_e260d36b](https://github.com/onatender/snakegame/assets/152275242/e4895232-d3f1-4d39-8b1c-492d9c2e9cdf)

