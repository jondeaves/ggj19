gource -1280x720 \
    --hide filenames,mouse,progress \
    --seconds-per-day 0.1 \
    --auto-skip-seconds 0.5 \
    --max-file-lag 0.1 \
    --title "Heckin Good Home Security"

ffmpeg -y \
    -r 60 \
    -f image2pipe \
    -vcodec ppm \
    -i gource.ppm \
    -vcodec libx264 \
    -preset medium \
    -pix_fmt yuv420p \
    -crf 1 \
    -threads 0 \
    -bf 0 \
    -crf 20 \
VID_20181122_181335.mp4
