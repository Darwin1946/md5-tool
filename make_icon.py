from PIL import Image, ImageDraw
im=Image.new('RGBA',(256,256),(0,0,0,0))
d=ImageDraw.Draw(im)
d.rounded_rectangle((8,8,248,248),radius=52,fill='#2563EB')
d.polygon([(66,42),(146,42),(186,82),(186,208),(66,208)],fill='white')
d.polygon([(146,42),(146,82),(186,82)],fill='#BFDBFE')
for y,w in [(105,73),(128,52),(151,38)]:
 d.rounded_rectangle((85,y,85+w,y+9),radius=4,fill='#93C5FD')
d.ellipse((133,139,231,237),fill='#16A36B',outline='#2563EB',width=7)
d.line([(158,186),(175,203),(207,170)],fill='white',width=12,joint='curve')
im.save('md5-tool/MD5Tool.ico',sizes=[(16,16),(24,24),(32,32),(48,48),(64,64),(128,128),(256,256)])
im.save('md5-tool/icon-preview.png')
