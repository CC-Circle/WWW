## シーン構造

シーン構造は以下に従います．（`Assets/Scenes/Template`で確認できます）

```
Cameras
Lights
Managers
Debug
UI
   Canvas
      Image
      ...
World
   Ground
   Props
   Structures
   ...
Gameplay
   Actors
   Items
   ...
Gameplay
   Actors
   Items
   ...
Dynamic Objects
```

### Cameras

ゲーム内で使用されるカメラやその設定を保存します．複数のカメラを使い分ける場合に，それぞれの設定を管理します．
今回のプロジェクトでは`MainCamera`と`VirtualCamera`以外に配置することはないと思います．

### Managers

ゲーム全体の状態や特定のシステムを管理するスクリプトやアセットを保存します．例えば，`GameManager`や`AudioManager`などがここに入ります．
`M5stack_Event` もここに配置することにします．

### Lights

ライティングに関連するアセットや設定を保存します.シーンの雰囲気や明暗を調整するライトのプレハブや設定がここに含まれます.

<details><summary>Lightingに関する注意点</summary>

`Assets/Scenes/`下に`Start`．`Main`，`End`フォルダがあると思いますが，これは Lighting の設定を行なっているフォルダなので削除しないでください．

もし，削除してしまった場合は，対象のシーンに入った状態で`window/Rendering/Kightnig`の GenerateLighting を行ってください．

</details>

### UI

ユーザーインターフェースに関連するアセットをまとめています．

#### Canvas

通常の Canvas と同じ使い方で結構です．

### World

ゲームの世界に関連するアセットを管理します．

#### Ground

地面や地形に関連するアセットを保存します．

#### Props

小道具や装飾的なオブジェクトを保存します．

#### Structures

建物やその他の構造物に関連するアセットを保存します．

### Gameplay

ゲームプレイに直接関連するアセットを保存します

#### Actors

プレイヤーキャラクターや NPC など，ゲームのキャラクターに関連するアセットを保存します．今回だったら Player や Enemy など．

#### Items

ゲーム内で使用されるアイテム（武器，道具，耗品など）に関連するアセットを保存します．

### Dynamic Objects

ゲーム中に動的に生成されたオブジェクトを保存します．
