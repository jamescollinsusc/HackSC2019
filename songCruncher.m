%Import table of frequencies for each note
noteFreqs  = csvread('noteFreqs.data');
amp_cutoff = .1;
neighborhood_size = 2000;

%Import song
[y,Fs] = audioread('Kept.mp3');

%Shorten song
%yshort = y(1:Fs*2,:);
yshort = y;


%Filter design
nyquist = Fs/2;
bl = zeros(length(noteFreqs),3);
al = zeros(length(noteFreqs),3);
bh = zeros(length(noteFreqs),3);
ah = zeros(length(noteFreqs),3);
notes = zeros(1,2);

for i=50:length(noteFreqs)
    if i==1
        lint = noteFreqs(i)/2;
        hint = (noteFreqs(i) + noteFreqs(i+1))/2;
    elseif i < length(noteFreqs)
        lint = (noteFreqs(i) + noteFreqs(i-1))/2;
        hint = (noteFreqs(i) + noteFreqs(i+1))/2;
    else
        lint = (noteFreqs(i) + noteFreqs(i-1))/2;
        hint = noteFreqs(i)*2;
    end
    Wnl = lint/nyquist;
    Wnh = hint/nyquist;
    [bl(i,:),al(i,:)] = butter(2,Wnh,'low');
    [bh(i,:),ah(i,:)] = butter(2,Wnl,'high');
    
    
    
    sfilt1 = filtfilt(bh(i,:),ah(i,:),yshort);
    sfilt2 = filtfilt(bl(i,:),al(i,:),sfilt1);
    hiamp = zeros(length(sfilt2), 2);
    %select high-amplitude portions of the sample
    [hiamp_row, col] = find(abs(sfilt2)>amp_cutoff);
    hiamp_idx = 1;
    hiamp(hiamp_idx:(hiamp_idx+neighborhood_size),:) = sfilt2(hiamp_row-neighborhood_size/2:hiamp_row+neighborhood_size/2,:);
    hiamp_idx = hiamp_idx + neighborhood_size;
    for j=2:length(hiamp_row)
        if (hiamp_row(j) - neighborhood_size/2 >= hiamp_row(j - 1))
            hiamp(hiamp_idx:(hiamp_idx+neighborhood_size),:) = sfilt2(hiamp_row(j)-neighborhood_size/2:hiamp_row(j)+neighborhood_size/2,:);
            hiamp_idx = hiamp_idx + neighborhood_size;
            j
        end
    end
    i

    hiamp = hiamp/max(max(hiamp));
    
    notes = [notes;hiamp];

% 
%     figure(1)
%     plot(sfilt2)
%     
%     
%     
%     sound(sfilt2,Fs)
%     
%     
%     %pause for of tone
%     pause(length(yshort)/Fs)

    
end



figure(1)
plot(notes)

    
        

    




%sound(yshort,Fs)






