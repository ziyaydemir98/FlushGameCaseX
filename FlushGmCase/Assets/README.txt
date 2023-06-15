Oyun İçerisine daha fazla gem eklemek için Project penceresinden sağ tık ile yeni bir scriptable "GemType" oluşturmak gerekiyor.
Oluşturulan SO.cs dosyasını düzenledikten sonra Art>Prefab sekmesinde GemPrafab içerisindeki listeye koymalısınız.
Oluşturulan yeni gem için UI sekmesinde Serializable yapıda olan Elements List içerisinden yeni bir eleman eklemelisiniz.
Tarla boyut ve offset düzeni için Hireyarşi > GAME > FieldManager objesine gidiniz.
Hucrelerın boyutunu buyutmek için Prefab klasorunden CellBox objesinin Scale ayarlarını değiştirebilirsiniz.
Hücre materyalini ve hücre objesini değiştirmek için yine FieldManager objesine gidiniz.