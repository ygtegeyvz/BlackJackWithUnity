# BlackJackWithUnity
İskambil kağıtları için genel (generic) bir data yapısı tasarlamanızı istiyoruz. Bu hazırlamış olduğunuz data yapısını, 21 oyununa göre nasıl implemente edersiniz?

Amaç bir oyun geliştirmek olduğu için son zamanlarda öğrenmeye çalıştığım Unity üstünden ilerledim.

Kart Modeli olarak dizi şeklinde kartların ön yüzleri ve tekil olarak arka yüzü ayrıca cardIndex'i kullandım.

Deste modeli oluşturup kartları bu deste modeline verdim.Bu deste modelini de hem oyuncunun hem kasanın hem de yere açılan destenin oluşturulmasında kullandım.
Deste modelinde ayrıca oyunun akışında kullanmak üzere boolean olarak oyunDestesiMi ve kartMevcutMu şeklinde 2 alan daha tuttum.

Not:Oyunun exe dosyası BlackJack Game klasöründe bulunmaktadır.
