   M S B A M L   `   `   `  ����   TI2    TI_lab2.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$:  4Шифровщик Теории Информации��$	��800��$
��1200��
�������.��� $��10q� � ColumnDefinitions �� $
��Auto ��� $��* �.+��� $��0��$��10q����#F0F4F8$	��280��.+��� $8��2Начальное состояние (29 бит):��$��Arial)�$��16(�$��5q��� -   $��tbxRegister�� �� TextWrapping$
 Wrap=�$	��260��$��30��$��Arial)�$��16(�$��5q��� $"��Длина регистра:��$��Arial)�$��16(�$��5q��� -   $��	tbxLength�� �� 
IsReadOnly$
 True��$
 Wrap=�$	��260��$��30��$��Arial)�$��16(�$��5q��� -   $��btnOpenFile��$��Открыть файл��$	��260��$��35��$��Arial)�$��16(�$��5q����#D9EFFF��  �� Orientation$ 
Horizontal=�$��Center=�$��5q�.+��� -   $��
btnEncrypt��$��Зашифровать��$	��120��$��35��$��Arial)�$��16(�$��5q����#D9EFFF�� -   $��
btnDecrypt��$��Дешифровать��$	��120��$��35��$��Arial)�$��16(�$��5q����#D9EFFF� $��1��$��10q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+��� $��	Ключ:��$��Arial)�$��16(�$��5q�$��0���� -   $��
rtbKeyText��$
 True�����#FFF8F0$��Arial)�$��16(�$��5q�$��1��$	��100��. �/� .T�J� .*�.*��� $� ���� $"��Исходный текст:��$��Arial)�$��16(�$��5q�$��2���� -   $��rtbOpenText��$
 True�����#FFF8F0$��Arial)�$��16(�$��5q�$��3��$	��200��. �/� .T�J� .*�.*��� $� ���� $"��Итоговый текст:��$��Arial)�$��16(�$��5q�$��4���� -   $��rtbCipherText��$
 True�����#FFF8F0$��Arial)�$��16(�$��5q�$��5��. �/� .T�J� .*�.*��� $� ��